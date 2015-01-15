using FactorioModBuilder.Build.Data;
using FactorioModBuilder.Build.Extensions;
using FactorioModBuilder.Build.Messages;
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
        }

        public bool Build(List<DataUnit> data)
        {
            // process each of our compilation units
            foreach(var c in data)
            {
                if (!this.CanContinue())
                    return false;

                ICompilerExtension ext;
                if(!_activeExtensions.TryGetValue(c.Type, out ext))
                {
                    this.BuildMessages.Add(
                        new ErrorMessage("Could not find appropriate extension to handle project data"));
                }
                else
                {
                    if(!ext.BuildUnit(c))
                    {
                        this.BuildMessages.Add(new ErrorMessage(
                            "Extension: {0} failed to build {1}", ext.Extension, c.GetType().Name));
                    }
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

        public ICompilerExtension GetExtension(ExtensionType type)
        {
            return _activeExtensions[type];
        }

        public bool CanContinue()
        {
            if (this.BuildMessages.Where(o => o.Type == MessageType.Fatal).Any())
                return false;
            if (this.BuildMessages.Where(o => o.Type == MessageType.Error).Count() > this.MaxErrors)
                return false;
            return true;
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
                foreach(var s in src)
                {
                    bool depChk = true;
                    foreach (var d in s.Dependencies)
                        depChk &= curExts.Contains(d);
                    if (depChk)
                        res.Add(s);
                }

                if (res.Count == oldCnt)
                    throw new Exception("Unabled to determine dependency tree");
            }

            return res;
        }
    }
}
