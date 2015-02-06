using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class EquipmentEnergySourceVM : ProjectItem<EquipmentEnergySource, EquipmentEnergySourceVM>
    {
        /// <summary>
        /// The type of this energy source.  Currently only electric is available
        /// </summary>
        public EnergySourceType Type
        {
            get { return this.GetProperty<EnergySourceType>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The maximum amount of energy this equipment can store
        /// </summary>
        public int BufferCapacity
        {
            get { return this.GetProperty<int>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The unit of the BufferCapacity value
        /// </summary>
        public EnergyUnit BufferCapUnit
        {
            get { return this.GetProperty<EnergyUnit>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// Whether or not this equipment types has a buffer capacity
        /// </summary>
        public bool HasBufferCap
        {
            get { return this.GetProperty<bool>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The maximum energy input that this equipment can sustain
        /// </summary>
        public int InputFlowLimit
        {
            get { return this.GetProperty<int>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The unit of the InputFlowLimit value
        /// </summary>
        public PowerUnit InputFlowLimitUnit
        {
            get { return this.GetProperty<PowerUnit>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// Whether or not this equipment types has a input flow limit
        /// </summary>
        public bool HasInputLimit
        {
            get { return this.GetProperty<bool>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The maximum energy output that this equipment can sustain
        /// </summary>
        public int OutputFlowLimit
        {
            get { return this.GetProperty<int>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The unit of the OutputFlowLimit
        /// </summary>
        public EnergyUnit OutputFlowLimitUnit
        {
            get { return this.GetProperty<EnergyUnit>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// Whether or not this equipment types has a output flow limit
        /// </summary>
        public bool HasOutputLimit
        {
            get { return this.GetProperty<bool>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The priority of this energy source for use by the equipment
        /// </summary>
        public UsagePriority UsagePriority
        {
            get { return this.GetProperty<UsagePriority>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The type of the containing equipment
        /// </summary>
        public EquipmentType ParentEquipmentType
        {
            get { return this.GetProperty<EquipmentType>(); }
            set { this.SetProperty(value, false, null, this.UpdateSource); }
        }

        /// <summary>
        /// Usage priority of this energy source for the equipment
        /// </summary>
        public UsagePriority Priority
        {
            get { return this.GetProperty<UsagePriority>(); }
            set { this.SetProperty(value); }
        }

        public EquipmentEnergySourceVM(EquipmentEnergySource item)
            : base(item, DoubleClickBehavior.OpenParent)
        {
        }

        public EquipmentEnergySourceVM(TreeItemVMBase parent, EquipmentEnergySource item)
            : base(parent, item, DoubleClickBehavior.OpenParent)
        {
        }
        
        /// <summary>
        /// Sets the appropriate properties for each equipment type
        /// </summary>
        /// <param name="type"></param>
        private void UpdateSource(EquipmentType type)
        {
            switch (type)
            {
                case EquipmentType.NightVision:
                case EquipmentType.EnergyShield:
                    this.HasBufferCap = true;
                    this.HasInputLimit = true;
                    this.HasOutputLimit = false;
                    break;
                case EquipmentType.Battery:
                    this.HasBufferCap = true;
                    this.HasInputLimit = true;
                    this.HasOutputLimit = true;
                    break;
                case EquipmentType.SolarPanel:
                case EquipmentType.Generator:
                case EquipmentType.MovementBonus:
                    this.HasBufferCap = false;
                    this.HasInputLimit = false;
                    this.HasOutputLimit = false;
                    break;
                case EquipmentType.ActiveDefense:
                    this.HasBufferCap = true;
                    this.HasInputLimit = false;
                    this.HasOutputLimit = false;
                    break;
                default:
                    throw new Exception("Unhandled equipment type");
            }
        }
    }
}
