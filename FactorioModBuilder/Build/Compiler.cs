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
                // prepare for the build
                this.PreBuild(c);

                ICompilerExtension ext;
                if(!_activeExtensions.TryGetValue(c.Type, out ext))
                {

                }
                else
                {
                    
                }

                // finish up after our build
                this.PostBuild(c);
            }

            return true;
        }

        private void PreBuild(DataUnit unit)
        {
            ProjectData data = unit as ProjectData;
            if (data == null)
                throw new ArgumentException("c is not a ProjectData object");

            // get and validate our temporary directory
            var tmpDirInfo = new DirectoryInfo(data.BaseTempDirectory);
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
            var outDirInfo = new DirectoryInfo(data.BaseOutDirectory);
            if(!outDirInfo.Exists)
                outDirInfo.Create();
        }

        private void PostBuild(DataUnit unit)
        {
            ProjectData data = unit as ProjectData;
            if (data == null)
                throw new ArgumentException("c is not a ProjectData object");

            // move the temporary directory project contents to the output directory
            if (Directory.Exists(data.BaseOutDirectory))
                Directory.Delete(data.BaseOutDirectory, true);
            Directory.Move(data.BaseTempDirectory, data.BaseOutDirectory);
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
