using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems.Prototype
{
    public class EquipmentEnergySource
    {
        public EnergySourceType Type { get; private set; }
        
        public int BufferCapacity { get; private set; }
        public EnergyUnit BufferCapUnit { get; private set; }

        public int InputFlowLimit { get; private set; }
        public EnergyUnit InputFlowLimitUnit { get; private set; }

        public int OutputFlowLimit { get; private set; }
        public EnergyUnit OutputFlowLimitUnit { get; private set; }

        public UsagePriority Priority { get; private set; }

        public EquipmentEnergySource(EnergySourceType type, int bufferCap, EnergyUnit bufferCapUnit,
            int inFlowLmt, EnergyUnit inFlowLmtUnit, int outFlowLmt, EnergyUnit outFlowLmtUnit,
            UsagePriority priority)
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
