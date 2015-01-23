using FactorioModBuilder.Build.Data;
using FactorioModBuilder.Build.Messages;
using FactorioModBuilder.Models.ProjectItems.Prototype;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class FluidsExtension : ExtensionBase<FluidData>
    {
        public FluidsExtension()
            : base(ExtensionType.PrototypeFluids, ExtensionType.Prototypes)
        {
        }

        protected override bool BuildUnit(IEnumerable<FluidData> units, StringBuilder sb)
        {
            sb.AppendLine("data:extend(");
            sb.AppendLine("{");
            sb.AppendLine(units.Select(o => this.GetOutput(o)).Aggregate((a, b) => a + "," + Environment.NewLine + b));
            sb.AppendLine("})");

            string res = sb.ToString();

            return true;
        }

        protected override bool ValidateData(IEnumerable<FluidData> units)
        {
            foreach(var d in units)
            {
                if(d.MaxTemp < d.DefaultTemp)
                    this.Warning(WarningLevel.W1, "Max temp is lower than default temp for fluid {0}", d.Name);
                if(d.HeatCapacity < 0)
                {
                    this.Error("Fluid {0} has a negative heat capacity value: {1}", d.Name, d.HeatCapacity);
                    return false;
                }
                if(!this.GraphicsPathLookup.ContainsKey(d.IconPath))
                {
                    this.Error("Fluid {0} contains an unknown icon path: {1}", d.Name, d.IconPath);
                    return false;
                }
            }
            return true;
        }

        protected override bool GetOutputPath(out string path)
        {
            path = Path.Combine(this.PrototypeDirectory, "fluids.lua");
            return true;
        }

        private string GetOutput(FluidData d)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("  {");
            sb.AppendLine("    type = \"fluid\",");
            sb.AppendLine("    name = \"" + d.Name + "\",");
            sb.AppendLine("    default_temperature = " + d.DefaultTemp + ",");
            sb.AppendLine("    heat_capacity = \"" + this.GetHeatCapacity(d.HeatCapacity, d.HeatCapacityUnit) + "\",");
            sb.AppendLine("    base_color = {r=" + d.BaseR + ", g=" + d.BaseG + ", b=" + d.BaseB + "},");
            sb.AppendLine("    flow_color = {r=" + d.FlowR + ", g=" + d.FlowG + ", b=" + d.FlowB + "},");
            sb.AppendLine("    max_temperature = " + d.MaxTemp + ",");
            sb.AppendLine("    icon = \"" + this.GraphicsPathLookup[d.IconPath] + "\",");
            sb.AppendLine("    pressure_to_speed_ratio = " + d.PressureToSpeedRatio + ",");
            sb.AppendLine("    flow_to_energy_ratio = " + d.FlowToEnergyRatio + ",");
            sb.AppendLine("    order = \"" + d.Order + "\"");
            sb.Append("  }");

            return sb.ToString();
        }

        private string GetHeatCapacity(int val, EnergyUnit unit)
        {
            switch (unit)
            {
                case EnergyUnit.Joule: return val + "J";
                case EnergyUnit.KiloJoule: return val + "KJ";
                case EnergyUnit.MegaJoule: return val + "MJ";
                default:
                    throw new InvalidOperationException("Unknown EnergyUnit type");
            }
        }
    }
}
