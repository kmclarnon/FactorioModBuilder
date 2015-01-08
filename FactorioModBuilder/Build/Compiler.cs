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

        public Compiler()
            : this(0)
        {
        }

        public Compiler(int maxErrors)
            : this(maxErrors, new List<ICompilerExtension>())
        {
        }

        public Compiler(int maxErrors, IEnumerable<ICompilerExtension> exts)
        {
            this.MaxErrors = maxErrors;
            this.BuildMessages = new List<CompilerMessage>();
            foreach (var ext in exts)
                this.AddExtension(ext);
        }

        public bool Build(string outDir, string tmpDir, List<CompileUnit> data)
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
            foreach(var c in data)
            {
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
                            if (!ext.BuildUnit(i.Value, out res))
                                this.BuildMessages.Add(new ErrorMessage("Failed to compile extension: " + i.Key));
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
