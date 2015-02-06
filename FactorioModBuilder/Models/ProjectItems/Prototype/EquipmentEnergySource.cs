using FactorioModBuilder.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems.Prototype
{
    public class EquipmentEnergySource : TreeItem<EquipmentEnergySource>
    {
        public EnergySourceType Type { get; private set; }
        
        public int BufferCapacity { get; private set; }
        public EnergyUnit BufferCapUnit { get; private set; }

        public int InputFlowLimit { get; private set; }
        public PowerUnit InputFlowLimitUnit { get; private set; }

        public int OutputFlowLimit { get; private set; }
        public PowerUnit OutputFlowLimitUnit { get; private set; }

        public UsagePriority Priority { get; private set; }

        public EquipmentEnergySource()
            : base("Energy Source")
        {
        }

        public EquipmentEnergySource(EnergySourceType type, int bufferCap, EnergyUnit bufferCapUnit,
            int inFlowLmt, PowerUnit inFlowLmtUnit, int outFlowLmt, PowerUnit outFlowLmtUnit,
            UsagePriority priority)
            : base("Energy Source")
        {
            this.Type = type;
            this.BufferCapacity = bufferCap;
            this.BufferCapUnit = bufferCapUnit;

            this.InputFlowLimit = inFlowLmt;
            this.InputFlowLimitUnit = inFlowLmtUnit;

            this.OutputFlowLimit = outFlowLmt;
            this.OutputFlowLimitUnit = outFlowLmtUnit;

            this.Priority = priority;
        }
    }
}
