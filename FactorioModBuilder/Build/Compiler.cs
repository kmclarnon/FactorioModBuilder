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

        public int MaxErrorContinuation { get; set; }

        private Dictionary<string, ICompilerExtension> _activeExtensions = new Dictionary<string,ICompilerExtension>();

        public Compiler()
        {
            this.BuildMessages = new List<CompilerMessage>();
        }

        public bool Build(string outDir, string tmpDir, Dictionary<string, CompileUnit> data)
        {
            // get and validate our temporary directory
            var tmpDirInfo = new DirectoryInfo(tmpDir);
            if(!tmpDirInfo.Exists)
            {
                tmpDirInfo.Create();
            }
            else
            {
                // clear out the temporary directory
                foreach (var file in tmpDirInfo.GetFiles())
                    file.Delete();
                foreach (var dir in tmpDirInfo.GetDirectories())
                    dir.Delete();
            }

            string curPath = "";
            // process each of our compilation units
            foreach(var kvp in data)
            {
                ICompilerExtension ext;
                if(!_activeExtensions.TryGetValue(kvp.Key, out ext))
                {
                    this.BuildMessages.Add(new ErrorMessage("Could not find compiler extension to support type: " + kvp.Key));
                    if(BuildMessages.Where(o => o.Type == CompilerMessage.MessageType.Error)
                        .Count() > this.MaxErrorContinuation)
                    {
                        this.BuildMessages.Add(new ErrorMessage("Encountered greater than " + 
                            this.MaxErrorContinuation + " errors. Build process halted."));
                        return false;
                    }
                }

                if(ext.SeparateFile)
                {
                    curPath = Path.Combine(tmpDir, ext.Filename);
                }

                try
                {
                    using (var fs = File.Open(curPath, FileMode.Append))
                    using (var sw = new StreamWriter(fs))
                    {
                        string res;
                        if (!ext.BuildUnit(kvp.Value, out res))
                            this.BuildMessages.Add(new ErrorMessage("Failed to compile extension: " + kvp.Key));
                        else
                            sw.Write(res);
                    }
                }
                catch (Exception e)
                {
                    this.BuildMessages.Add(new ErrorMessage("Encountered internal compiler exception: " + e.Message));
                    this.BuildMessages.Add(new ErrorMessage("Compiler encoutered a fatal error, build halted."));
                    return false;
                }
            }

            return true;
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
