using FactorioModBuilder.Build.Data;
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
        public PrototypesExtension()
            : base(ExtensionType.Prototypes)
        {
        }

        public override bool BuildUnit(DataUnit unit, DirectoryInfo outDir)
        {
            var pd = unit as PrototypesData;
            if(pd == null)
            {
                this.Error("Expected intput to be prototypes data, recieved: {0}", unit.GetType().FullName);
                return false;
            }

            // create our prototypes directory
            var di = this.CreateCleanDirectory(Path.Combine(outDir.FullName, "prototypes"));
            foreach(var s in pd.SubUnits.Where(o => 
                o.Type != ExtensionType.PrototypeGroups && 
                o.Type != ExtensionType.PrototypeSubgroups))
            {
                if (!this.CanContinue())
                    return false;

                if (s == null)
                {
                    this.Error("Encountered null subunit in project data");
                    continue;
                }

                ICompilerExtension ext;
                if(!this.TryGetCompilerExtension(s.Type, out ext))
                    this.Error("Could not find appropriate extension for: {0}", s.Type);
                else
                {
                    switch (s.Type)
                    {
                        case ExtensionType.PrototypeEntities:
                        case ExtensionType.PrototypeEquipment:
                        case ExtensionType.PrototypeFluids:
                        case ExtensionType.PrototypeItems:
                        case ExtensionType.PrototypeRecipes:
                        case ExtensionType.PrototypeTechnologies:
                        case ExtensionType.PrototypeTiles:
                            if (!ext.BuildUnit(s, di))
                            {
                                this.Error("Failed to build: {0}", ext.Extension);
                                continue;
                            }
                            break;
                        default:
                            throw new InvalidOperationException("Unknown Prototype extension type: " + s.Type);
                    }
                }
            }

            var elms = pd.SubUnits.Where(o => 
                o.Type == ExtensionType.PrototypeSubgroups || 
                o.Type == ExtensionType.PrototypeGroups);
            if(elms.Any())
            {
                ICompilerExtension ext;
                if (!this.TryGetCompilerExtension(ExtensionType.PrototypeGroups, out ext))
                    this.Error("Could not find appropriate extension for: PrototypeGroups");
                else
                {
                    if(!ext.BuildUnit(elms, di))
                        this.Error("Failed to build: {0}", ext.Extension);
                }
            }

            return true;
        }

        public override bool BuildUnit(DataUnit unit, out string value)
        {
            throw new NotImplementedException();
        }
    }
}
