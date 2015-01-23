using FactorioModBuilder.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FactorioModBuilder.Models.ProjectItems.Prototype
{
    public enum EnergyUnit
    {
        [DescriptionAttribute("J")] 
        Joule,
        [DescriptionAttribute("kJ")]
        KiloJoule,
        [DescriptionAttribute("MJ")]
        MegaJoule
    }

    public class Fluid : TreeItem<Fluid>
    {
        public int DefaultTemp { get; set; }
        
        public int HeatCapacity { get; set; }
        public EnergyUnit HeatCapacityUnit { get; set; }

        public Color BaseColor { get; set; }
        public Color FlowColor { get; set; }
        
        public int MaxTemp { get; set; }
        public string IconPath { get; set; }
        
        public float PressureToSpeed { get; set; }
        public float FlowToEnergy { get; set; }
        
        public string Order { get; set; }
        public string SubGroup { get; set; }

        public Fluid(string name) : base(name)
        {
        }
    }
}
