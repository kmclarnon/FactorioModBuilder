using FactorioModBuilder.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems.Prototype
{
    public class Fluid : TreeItem<Fluid>
    {
        public enum EnergyUnit
        {
            Joule,
            KiloJoule,
            MegaJoule
        }

        public int DefaultTemp { get; set; }
        
        public int HeatCapacity { get; set; }
        public EnergyUnit HeatCapacityUnit { get; set; }

        public float BaseColorR { get; set; }
        public float BaseColorG { get; set; }
        public float BaseColorB { get; set; }
        
        public float FlowColorR { get; set; }
        public float FlowColorG { get; set; }
        public float FlowColorB { get; set; }
        
        public int MaxTemp { get; set; }
        public string IconPath { get; set; }
        
        public float PressureToSpeed { get; set; }
        public float FlowToEnergy { get; set; }
        
        public string Order { get; set; }

        public Fluid(string name) : base(name)
        {
        }
    }
}
