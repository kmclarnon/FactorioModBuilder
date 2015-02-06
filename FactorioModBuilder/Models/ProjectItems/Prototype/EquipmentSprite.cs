using FactorioModBuilder.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems.Prototype
{
    public class EquipmentSprite : TreeItem<EquipmentSprite>
    {
        public string FileName { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public SpritePriority Priority { get; private set; }

        public EquipmentSprite()
            : base("Sprite")
        {
        }

        public EquipmentSprite(string filename, int width, int height, SpritePriority priority)
            : base("Sprite")
        {
            this.FileName = filename;
            this.Width = width;
            this.Height = height;
            this.Priority = priority;
        }
    }
}
