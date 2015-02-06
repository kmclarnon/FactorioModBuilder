using FactorioModBuilder.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems.Prototype
{
    public class EquipmentShape : TreeItem<EquipmentShape>
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public ShapeType Type { get; private set; }

        public EquipmentShape()
            : base("Shape")
        {
        }

        public EquipmentShape(int width, int height, ShapeType type)
            : base("Shape")
        {
            this.Width = width;
            this.Height = height;
            this.Type = type;
        }
    }
}
