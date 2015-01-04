using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems.Prototype
{
    public class TileVariant : ProjectItem<TileVariant>
    {
        public string Picture { get; set; }
        public int Count { get; set; }
        public int Size { get; set; }

        public TileVariant(string name) : base(name)
        {

        }
    }
}
