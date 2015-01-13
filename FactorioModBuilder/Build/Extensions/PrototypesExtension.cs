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
            foreach(var s in pd.SubUnits)
            {
                switch (s.Type)
                {
                    case ExtensionType.PrototypeEntities:
                        break;
                    case ExtensionType.PrototypeEquipment:
                        break;
                    case ExtensionType.PrototypeFluids:
                        break;
                    case ExtensionType.PrototypeGroups:
                        break;
                    case ExtensionType.PrototypeSubgroups:
                        break;
                    case ExtensionType.PrototypeItems:
                        break;
                    case ExtensionType.PrototypeRecipes:
                        break;
                    case ExtensionType.PrototypeTechnologies:
                        break;
                    case ExtensionType.PrototypeTiles:
                        break;
                    default:
                        throw new InvalidOperationException("Unknown Prototype extension type: " + s.Type);
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
