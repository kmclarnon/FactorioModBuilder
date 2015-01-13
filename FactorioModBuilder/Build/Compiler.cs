using FactorioModBuilder.Build.Data;
using FactorioModBuilder.Build.Extensions;
using FactorioModBuilder.Build.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build
{
    public class Compiler
    {
        public List<CompilerMessage> BuildMessages { get; private set; }

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
            this.BuildMessages = new List<CompilerMessage>();
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

        public bool CanContinue()
        {
            if (this.BuildMessages.Where(o => o.Type == MessageType.Fatal).Any())
                return false;
            if (this.BuildMessages.Where(o => o.Type == MessageType.Error).Count() > this.MaxErrors)
                return false;
            return true;
        }
    }
}
