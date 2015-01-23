using FactorioModBuilder.Build.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class TilesExtension : ExtensionBase<TileData>
    {
        public TilesExtension()
            : base(ExtensionType.PrototypeTiles, ExtensionType.Prototypes)
        {
        }

        protected override bool BuildUnit(IEnumerable<TileData> units, StringBuilder sb)
        {
            return true;
        }

        protected override bool ValidateData(IEnumerable<TileData> units)
        {
            return true;
        }

        protected override bool GetOutputPath(out string path)
        {
            path = Path.Combine(this.PrototypeDirectory, "tiles.lua");
            return true;
        }
    }
}
