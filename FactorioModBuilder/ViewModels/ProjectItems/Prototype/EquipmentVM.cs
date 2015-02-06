using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    /// <summary>
    /// A view model to wrap the Equipment model
    /// </summary>
    public class EquipmentVM : ProjectItem<Equipment, EquipmentVM>
    {
        /// <summary>
        /// The type parameter of the Equipment model
        /// </summary>
        public EquipmentType Type
        {
            get { return this.GetProperty<EquipmentType>(); }
            set { this.SetProperty(value, false, null, (x => this.EnergySource.ParentEquipmentType = x)); }
        }

        /// <summary>
        /// The sprite file and associated parameters
        /// </summary>
        public EquipmentSpriteVM Sprite { get; private set; }

        /// <summary>
        /// The shape of this equipment in the armor gui
        /// </summary>
        public EquipmentShapeVM Shape { get; private set; }

        /// <summary>
        /// The energy source of this equipment
        /// </summary>
        public EquipmentEnergySourceVM EnergySource { get; private set; }

        /// <summary>
        /// The max shield value of the Equipment model
        /// </summary>
        public int MaxShieldValue
        {
            get { return this.GetProperty<int>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The energy per shield value of the Equipment model
        /// </summary>
        public int EnergyPerShield
        {
            get { return this.GetProperty<int>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The energy that this device requires to activate
        /// </summary>
        public int EnergyInput
        {
            get { return this.GetProperty<int>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The unit of the EnergyInput property
        /// </summary>
        public PowerUnit EnergyInputUnit
        {
            get { return this.GetProperty<PowerUnit>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The basic constructor to wrap an Equipment model in a view model
        /// </summary>
        /// <param name="equip"></param>
        public EquipmentVM(Equipment equip)
            : this(null, equip)
        {
        }

        /// <summary>
        /// The constructor to wrap an Equipment model in a view model and associate it with
        /// the given parent
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="equip"></param>
        public EquipmentVM(TreeItemVMBase parent, Equipment equip)
            : base(parent, equip, DoubleClickBehavior.OpenContent)
        {
            this.EnergySource = new EquipmentEnergySourceVM(new EquipmentEnergySource());
            this.Shape = new EquipmentShapeVM(new EquipmentShape());
            this.Sprite = new EquipmentSpriteVM(new EquipmentSprite());
        }
    }
}
