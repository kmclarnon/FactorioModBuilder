using FactorioModBuilder.Build.Directives;
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

        public bool Build(List<CompileUnit> data)
        {
            // process each of our compilation units
            foreach(var c in data)
            {
                // prepare for the build
                var settings = this.PreBuild(c);

                

                // finish up after our build
                this.PostBuild(settings);
            }

            return true;
        }

        private CompilerSettings PreBuild(CompileUnit c)
        {
            CompilerSettings settings = new CompilerSettings();
            // get all of our compiler directives
            foreach(var cd in c.Directives)
            {
                switch (cd.Type)
                {
                    case CompilerDirective.DirectiveType.TemporaryDirectory:
                        settings.TempDir = cd.Data;
                        break;
                    case CompilerDirective.DirectiveType.OutputDirectory:
                        settings.OutDir = cd.Data;
                        break;
                    case CompilerDirective.DirectiveType.ProjectName:
                        settings.ProjectName = cd.Data;
                        break;
                }
            }

            // ensure that we have our required settings
            if (!settings.Complete)
                throw new Exception("Compiler settings are incomplete");

            // get and validate our temporary directory
            var tmpDirInfo = new DirectoryInfo(settings.BaseTempDirectory);
            if (!tmpDirInfo.Exists)
            {
                tmpDirInfo.Create();
            }
            else
            {
                // clear out the temporary directory
                foreach (var file in tmpDirInfo.GetFiles())
                    file.Delete();
                foreach (var dir in tmpDirInfo.GetDirectories())
                    dir.Delete(true);
            }

            // get and validate our output directory
            var outDirInfo = new DirectoryInfo(settings.BaseOutDirectory);
            if(!outDirInfo.Exists)
                outDirInfo.Create();

            return settings;
        }

        private void PostBuild(CompilerSettings settings)
        {
            // move the temporary directory project contents to the output directory
            if (Directory.Exists(settings.BaseOutDirectory))
                Directory.Delete(settings.BaseOutDirectory, true);
            Directory.Move(settings.BaseTempDirectory, settings.BaseOutDirectory);
        }

        public bool AddExtension(ICompilerExtension ext)
        {
            if (_activeExtensions.ContainsKey(ext.Extension))
                return false;
            ext.Parent = this;
            _activeExtensions.Add(ext.Extension, ext);
            return true;
        }

        public bool TryGetExtension(ExtensionType type, out ICompilerExtension ext)
        {
            return _activeExtensions.TryGetValue(type, out ext);
        }
    }
}
