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

        private Dictionary<ExtensionType, ICompilerExtension> _activeExtensions = 
            new Dictionary<ExtensionType, ICompilerExtension>();

        public string ProjectName { get; set; }
        public string TemporaryDirectory { get; set; }
        public string OutputDirectory { get; set; }
        public string PrototypeDirectory { get; set; }
        public string DependencyString { get; set; }
        public HashSet<string> GroupNames { get; private set; }
        public HashSet<string> SubGroupNames { get; private set; }
        public HashSet<string> ItemNames { get; private set; }
        public HashSet<string> EntityNames { get; private set; }

        public Compiler()
            : this(0)
        {
        }

        public Compiler(int maxErrors)
            : this(maxErrors, new List<ICompilerExtension>())
        {
        }

        public Compiler(int maxErrors, params ICompilerExtension[] exts)
            : this(maxErrors, exts.ToList())
        {
        }

        public Compiler(int maxErrors, IEnumerable<ICompilerExtension> exts)
        {
            this.MaxErrors = maxErrors;
            this.BuildMessages = new ObservableCollection<CompilerMessage>();
            foreach (var ext in exts)
                this.AddExtension(ext);


            this.GroupNames = new HashSet<string>();
            this.SubGroupNames = new HashSet<string>();
            this.ItemNames = new HashSet<string>();
        }

        public bool Build(List<DataUnit> data)
        {
            
            IEnumerable<ICompilerExtension> exts;
            if(!this.TryGetRequiredExtensions(data, out exts))
            {
                return false;
            }

            // process each of our compilation units
            foreach(var ext in this.GetProcessOrder(exts))
            {
                if (!this.CanContinue())
                    return false;

                if(!ext.BuildUnit(data.Where(o => o.Type == ext.Extension)))
                {
                    continue;
                }
            }

            return true;
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
            List<ICompilerExtension> res = new List<ICompilerExtension>();
            List<ICompilerExtension> src = extensions.ToList();

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
                foreach (var e in ext.Dependencies)
                {
                    if (!this.TryGetExtension(e, out ext))
                        return false;
                    res.Add(ext);
                }
            }

            result = res.DistinctBy(o => o.Extension);
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
            this.OutputDirectory = String.Empty;
            this.TemporaryDirectory = String.Empty;
            this.GroupNames = new HashSet<string>();
            this.SubGroupNames = new HashSet<string>();
            this.ItemNames = new HashSet<string>();
        }
    }
}
