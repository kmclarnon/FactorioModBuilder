using FactorioModBuilder.Build.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class PrototypesExtension : ExtensionBase
    {
        public override string SupportedUnitName
        {
            get { return "prototypes"; }
        }

        public override bool BuildUnit(CompileUnit unit, DirectoryInfo outDir)
        {
            if (unit.UType != CompileUnit.UnitType.Struct)
                throw new Exception("Unknown prototypes format");

            // modify our output directory so that the resulting files are in prototypes
            DirectoryInfo newDir = new DirectoryInfo(Path.Combine(outDir.FullName, "prototypes"));
            if(!newDir.Exists)
                newDir.Create();

            // all extensions are registered with the compiler and accessed through parent
            foreach(var kvp in unit.StructValues)
            {
                ICompilerExtension ext;
                if(this.Parent.TryGetExtension(kvp.Key, out ext))
                {
                    if (!ext.BuildUnit(kvp.Value, newDir))
                        this.Parent.BuildMessages.Add(new ErrorMessage("Failed to build: " + kvp.Key));
                }
                else
                {
                    this.Parent.BuildMessages.Add(new ErrorMessage(
                        "Could not find an appropriate compiler extension for: " + kvp.Key));
                }
            }

            return true;
        }
    }
}
