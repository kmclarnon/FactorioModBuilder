using FactorioModBuilder.Build.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class PrototypeEntitiesExtension : ExtensionBase<EntityData>
    {
        public PrototypeEntitiesExtension()
            : base(ExtensionType.PrototypeEntities, ExtensionType.Prototypes)
        {
        }

        protected override bool BuildUnit(IEnumerable<EntityData> units, StreamWriter sr)
        {
            sr.Write(" ");
            return true;
        }

        protected override bool GetOutputPath(out string path)
        {
            path = Path.Combine(this.PrototypeDirectory, "entities.lua");
            return true;
        }
    }
}
