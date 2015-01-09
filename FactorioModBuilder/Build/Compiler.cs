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

        private Dictionary<string, ICompilerExtension> _activeExtensions = new Dictionary<string,ICompilerExtension>();

        private class CompileSettings
        {
            public string TempDir { get; set; }
            public string OutDir { get; set; }
            public string ProjectName { get; set; }
            public string BaseTempDirectory { get { return Path.Combine(TempDir, ProjectName); } }
            public string BaseOutDirectory { get { return Path.Combine(OutDir, ProjectName); } }
            public bool Complete
            {
                get
                {
                    return TempDir != null && 
                        OutDir != null && ProjectName != null;
                }
            }
        }

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

                if(c.UType != CompileUnit.UnitType.Struct)
                    return false;
                foreach(var i in c.StructValues)
                {
                    // ensure that we can continue to compile
                    if (BuildMessages.Where(o => o.Type == CompilerMessage.MessageType.Error)
                        .Count() > this.MaxErrors)
                    {
                        this.BuildMessages.Add(new ErrorMessage("Encountered greater than " +
                            this.MaxErrors + " errors. Build process halted."));
                        return false;
                    }

                    ICompilerExtension ext;
                    if(!_activeExtensions.TryGetValue(i.Key, out ext))
                    {
                        this.BuildMessages.Add(new ErrorMessage("Could not find compiler extension to support type: " + i.Key));
                        // since we couldn't find an extension, we're done
                        continue;
                    }

                    if (!ext.BuildUnit(i.Value, new DirectoryInfo(settings.BaseTempDirectory)))
                        this.BuildMessages.Add(new ErrorMessage("Failed to compile extension: " + i.Key));
                }

                // finish up after our build
                this.PostBuild(settings);
            }

            return true;
        }

        private CompileSettings PreBuild(CompileUnit c)
        {
            CompileSettings settings = new CompileSettings();
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

        private void PostBuild(CompileSettings settings)
        {
            // move the temporary directory project contents to the output directory
            string srcPath = Path.Combine(settings.BaseTempDirectory, settings.ProjectName);
            string destPath = Path.Combine(settings.BaseOutDirectory, settings.ProjectName);
            if (Directory.Exists(destPath))
                Directory.Delete(destPath, true);
            Directory.CreateDirectory(destPath);
            Directory.Move(
                Path.Combine(settings.BaseTempDirectory, settings.ProjectName),
                Path.Combine(settings.BaseOutDirectory, settings.ProjectName));
        }

        public bool AddExtension(ICompilerExtension ext)
        {
            if (_activeExtensions.ContainsKey(ext.SupportedUnitName))
                return false;
            ext.Parent = this;
            _activeExtensions.Add(ext.SupportedUnitName, ext);
            return true;
        }

        public bool TryGetExtension(string name, out ICompilerExtension ext)
        {
            return _activeExtensions.TryGetValue(name, out ext);
        }
    }
}
