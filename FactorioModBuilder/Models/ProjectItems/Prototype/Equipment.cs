using FactorioModBuilder.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems.Prototype
{
    public class Equipment : TreeItem<Equipment>
    {
        public string Type { get; set; }
        public int EnergyInput { get; set; }
        public int ShapeWidth { get; set; }
        public int ShapeHeight { get; set; }
        public string ShapeType { get; set; }
        public string SpriteFilename { get; set; }
        public int SpriteWidth { get; set; }
        public int SpriteHeight { get; set; }
        public string SpritePriority { get; set; }
        public int MaxShieldValue { get; set; }
        public int EnergyPerShield { get; set; }
        public string EnergySourceType { get; set; }
        public string BufferCap { get; set; }
        public string InputLimit { get; set; }
        public string OutputLimit { get; set; }
        public string UsagePriority { get; set; }

        public Equipment(string name) : base(name)
        {
        }
    }
}
