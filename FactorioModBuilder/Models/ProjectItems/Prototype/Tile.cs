using FactorioModBuilder.Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FactorioModBuilder.Models.ProjectItems.Prototype
{
    public class Tile : TreeItem<Tile>
    {
        public string Type { get; set; }
        public int Layer { get; set; }
        public List<String> CollisionMask { get; private set; }
        public List<Tile> AllowedNeighboors { get; private set; }
        public Color MapColor { get; set; }
        public List<TileVariant> Main { get; private set; }
        public List<TileVariant> InnerCorner { get; private set; }
        public List<TileVariant> OuterCorner { get; private set; }
        public List<TileVariant> Side { get; private set; }

        public Tile(string name) : base(name)
        {
            this.CollisionMask = new List<string>();
            this.Main = new List<TileVariant>();
            this.InnerCorner = new List<TileVariant>();
            this.OuterCorner = new List<TileVariant>();
            this.Side = new List<TileVariant>();
        }
    }
}
