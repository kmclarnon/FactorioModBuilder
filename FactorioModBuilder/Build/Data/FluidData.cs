using FactorioModBuilder.Models.ProjectItems.Prototype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FactorioModBuilder.Build.Data
{
    public class FluidData : DataUnit
    {
        public string Name { get; private set; }

        public int HeatCapacity { get; private set; }
        public EnergyUnit HeatCapacityUnit { get; private set; }

        public float BaseR { get; private set; }
        public float BaseG { get; private set; }
        public float BaseB { get; private set; }
        
        public float FlowR { get; private set; }
        public float FlowG { get; private set; }
        public float FlowB { get; private set; }

        public int DefaultTemp { get; private set; }
        public int MaxTemp { get; private set; }

        public float PressureToSpeedRatio { get; private set; }
        public float FlowToEnergyRatio { get; set; }

        public string Order { get; set; }
        public string IconPath { get; set; }

        public FluidData(string name, int heatCap, EnergyUnit heatCapUnit, Color baseColor, 
            Color flowColor, int defTemp, int maxTemp, float pToSRatio,
            float ftoERatio, string order, string iconPath)
            : base(Extensions.ExtensionType.PrototypeFluids)
        {
            this.Name = name;
            this.HeatCapacity = heatCap;
            this.HeatCapacityUnit = heatCapUnit;
            
            this.BaseR = baseColor.R;
            this.BaseG = baseColor.G;
            this.BaseB = baseColor.B;
            
            this.FlowR = flowColor.R;
            this.FlowG = flowColor.G;
            this.FlowB = flowColor.B;

            this.DefaultTemp = defTemp;
            this.MaxTemp = maxTemp;
            this.PressureToSpeedRatio = pToSRatio;
            this.FlowToEnergyRatio = ftoERatio;
            this.Order = order;
            this.IconPath = iconPath;
        }
    }
}
