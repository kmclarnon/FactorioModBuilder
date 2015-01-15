using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class ItemsExtension : ExtensionBase
    {
        public ItemsExtension()
            : base(ExtensionType.PrototypeItems,
            ExtensionType.PrototypeEntities)
        {
        }
    }
}
