using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems.Prototype
{
    public class EquipmentShape
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public ShapeType Type { get; private set; }

        public EquipmentShape(int width, int height, ShapeType type)
        {
            this.Width = width;
            this.Height = height;
            this.Type = type;
        }
    }
}
