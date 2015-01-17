using FactorioModBuilder.Build.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class ItemsExtension : ExtensionBase<ItemData>
    {
        public ItemsExtension()
            : base(ExtensionType.PrototypeItems,
            ExtensionType.PrototypeEntities)
        {
        }

        protected override bool BuildUnit(IEnumerable<ItemData> units, StreamWriter sw)
        {
            throw new NotImplementedException();
        }
    }
}
