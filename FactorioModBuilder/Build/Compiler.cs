using FactorioModBuilder.Build.Data;
using FactorioModBuilder.Build.Extensions;
using FactorioModBuilder.Build.Messages;
using FactorioModBuilder.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build
{
    public class Compiler
    {
        public ObservableCollection<CompilerMessage> BuildMessages { get; private set; }

        public int MaxErrors { get; set; }
        public bool EnableBuildValidation { get; set; }

        private Dictionary<ExtensionType, ICompilerExtension> _activeExtensions = 
            new Dictionary<ExtensionType, ICompilerExtension>();

        public string ProjectName { get; set; }
        public string ProjectVersion { get; set; }
        public string ProjectDirectory { get; set; }
        public string TemporaryDirectory { get; set; }
        public string OutputDirectory { get; set; }
        public string PrototypeDirectory { get; set; }
        public string DependencyString { get; set; }
        public HashSet<string> GroupNames { get; private set; }
        public HashSet<string> SubGroupNames { get; private set; }
        public HashSet<string> ItemNames { get; private set; }
        public HashSet<string> EntityNames { get; private set; }
        public Dictionary<string, string> GraphicsPathLookup { get; private set; }
        public HashSet<string> GeneratedFiles { get; private set; }
        
        public Compiler()
            : this(0)
        {
        }

        public Compiler(int maxErrors)
            : this(maxErrors, new List<ICompilerExtension>())
        {
        }

        public Compiler(int maxErrors, bool enableChecks)
            : this(maxErrors, enableChecks, new List<ICompilerExtension>())
        {
        }

        public Compiler(int maxErrors, params ICompilerExtension[] exts)
            : this(maxErrors, exts.ToList())
        {
        }

        public Compiler(int maxErrors, IEnumerable<ICompilerExtension> exts)
            : this(maxErrors, true, exts)
        {
        }

        public Compiler(int maxErrors, bool enableChecks, params ICompilerExtension[] exts)
            : this(maxErrors, enableChecks, exts.ToList())
        {
        }

        public Compiler(int maxErrors, bool enableChecks, IEnumerable<ICompilerExtension> exts)
        {
            this.MaxErrors = maxErrors;
            this.BuildMessages = new ObservableCollection<CompilerMessage>();
            foreach (var ext in exts)
                this.AddExtension(ext);

            this.GroupNames = new HashSet<string>();
            this.SubGroupNames = new HashSet<string>();
            this.ItemNames = new HashSet<string>();
            this.EntityNames = new HashSet<string>();
            this.GraphicsPathLookup = new Dictionary<string, string>();
            this.GeneratedFiles = new HashSet<string>();
        }

        public bool Build(List<DataUnit> data)
        {
            try
            {
                // clear out any previous messages
                this.BuildMessages.Clear();
                this.ResetProjectStorage();
                IEnumerable<ICompilerExtension> exts;
                if (!this.TryGetRequiredExtensions(data, out exts))
                {
                    this.BuildMessages.Add(new FatalMessage(
                        "The compiler could not find all required extensions for the project data"));
                    return false;
                }

                // process each of our compilation units
                foreach (var ext in this.GetProcessOrder(exts))
                {
                    if (!this.CanContinue())
                    {
                        this.BuildMessages.Add(new ErrorMessage(
                            "The compiler cannot continue due to previous errors encountered, build halted"));
                        return false;
                    }

                    if (!ext.BuildUnit(data.Where(o => o.Type == ext.Extension)))
                    {
                        this.BuildMessages.Add(new ErrorMessage(
                            "Extension " + ext.GetType().FullName + " failed to compile " + ext.Extension + " data"));
                        continue;
                    }
                }

                this.BuildMessages.Add(new InfoMessage(
                    "Project " + this.ProjectName + " compiled successfully"));
                return true;
            }
            catch(Exception e)
            {
                this.BuildMessages.Add(new FatalMessage(
                    "The compiler encountered a fatal exception during the build: " + e.Message));
                return false;
            }
        }

        public bool AddExtension(ICompilerExtension ext)
        {
            if (_activeExtensions.ContainsKey(ext.Extension))
                return false;
            ext.AttachToCompiler(this);
            _activeExtensions.Add(ext.Extension, ext);
            return true;
        }

        public bool TryGetExtension(ExtensionType type, out ICompilerExtension ext)
        {
            return _activeExtensions.TryGetValue(type, out ext);
        }

        public IEnumerable<ICompilerExtension> GetProcessOrder(IEnumerable<ICompilerExtension> extensions)
        {
            if (extensions == null)
                throw new ArgumentNullException("extensions");
            List<ICompilerExtension> src = extensions.ToList();
            HashSet<ICompilerExtension> res = new HashSet<ICompilerExtension>(src.Where(o => !o.Dependencies.Any()));

            int srcCnt = src.Count;
            int oldCnt = res.Count;
            while(res.Count < srcCnt)
            {
                var curExts = res.Select(o => o.Extension);
                foreach(var s in src.Where(o => !res.Contains(o)))
                {
                    bool depChk = true;
                    foreach (var d in s.Dependencies)
                        depChk &= curExts.Contains(d);
                    if (depChk)
                        res.Add(s);
                }

                if (res.Count == oldCnt)
                    throw new Exception("Unabled to determine dependency tree");
                oldCnt = res.Count;
            }

            return res;
        }

        private bool TryGetRequiredExtensions(IEnumerable<DataUnit> data, out IEnumerable<ICompilerExtension> result)
        {
            result = null;
            var res = new List<ICompilerExtension>();
            foreach(var d in data.DistinctBy(o => o.Type))
            {
                ICompilerExtension ext;
                if (!this.TryGetExtension(d.Type, out ext))
                    return false;
                res.Add(ext);
                IEnumerable<ICompilerExtension> deps;
                if (!this.TryGetDependencies(ext, out deps))
                    return false;
                res.AddRange(deps);
            }

            result = res.DistinctBy(o => o.Extension);
            return true;
        }

        private bool TryGetDependencies(ICompilerExtension ext, out IEnumerable<ICompilerExtension> result)
        {
            result = null;
            if (ext == null)
                return false;

            List<ICompilerExtension> res = new List<ICompilerExtension>();
            foreach(var d in ext.Dependencies)
            {
                ICompilerExtension tmpExt;
                if(!this.TryGetExtension(d, out tmpExt))
                    return false;
                res.Add(tmpExt);

                IEnumerable<ICompilerExtension> deps;
                if (!this.TryGetDependencies(tmpExt, out deps))
                    return false;
                res.AddRange(deps);
            }
            result = res;
            return true;
        }

        private bool CanContinue()
        {
            if (this.BuildMessages.Where(o => o.Type == MessageType.Fatal).Any())
                return false;
            if (this.BuildMessages.Where(o => o.Type == MessageType.Error).Count() > this.MaxErrors)
                return false;
            return true;
        }

        private void ResetProjectStorage()
        {
            this.ProjectName = String.Empty;
            this.ProjectVersion = String.Empty;
            this.ProjectDirectory = String.Empty;
            this.PrototypeDirectory = String.Empty;
            this.OutputDirectory = String.Empty;
            this.TemporaryDirectory = String.Empty;
            this.DependencyString = String.Empty;

            this.GroupNames.Clear();
            this.SubGroupNames.Clear();
            this.ItemNames.Clear();
            this.EntityNames.Clear();
            this.GraphicsPathLookup.Clear();
            this.GeneratedFiles.Clear();
        }
    }
}
