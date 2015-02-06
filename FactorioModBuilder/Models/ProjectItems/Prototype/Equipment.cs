using FactorioModBuilder.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems.Prototype
{
    public enum SpritePriority
    {
        [DescriptionAttribute("Medium")]
        Medium
    }

    public enum ShapeType
    {
        [DescriptionAttribute("Full")]
        Full
    }

    public enum EnergySourceType
    {
        [DescriptionAttribute("Electric")]
        Electric
    }

    public enum UsagePriority
    {
        [DescriptionAttribute("Primary Input")]
        PrimaryInput,
        [DescriptionAttribute("Secondary")]
        Secondary,
        [DescriptionAttribute("Terciary")]
        Terciary
    }

    public enum EquipmentType
    {
        [DescriptionAttribute("Night Vision Equipment")]
        NightVision,
        [DescriptionAttribute("Energy Shield Equipment")]
        EnergyShield,
        [DescriptionAttribute("Battery Equipment")]
        Battery,
        [DescriptionAttribute("Solar Panel Equipment")]
        SolarPanel,
        [DescriptionAttribute("Generator Equipment")]
        Generator,
        [DescriptionAttribute("Active Defense Equipment")]
        ActiveDefense,
        [DescriptionAttribute("Movement Bonus Equipment")]
        MovementBonus
    }

    public enum PowerUnit
    {
        [DescriptionAttribute("W")]
        Watt,
        [DescriptionAttribute("kW")]
        KiloWatt,
        [DescriptionAttribute("MW")]
        MegaWatt
    }

    public class Equipment : TreeItem<Equipment>
    {
        public EquipmentType Type { get; set; }
        public int EnergyInput { get; set; }
        public EquipmentShape Shape { get; private set; }
        public EquipmentSprite Sprite { get; private set; }
        public int MaxShieldValue { get; set; }
        public int EnergyPerShield { get; set; }
        public EquipmentEnergySource EnergySource { get; private set; }
        public string UsagePriority { get; set; }

        public Equipment(string name) : base(name)
        {
            this.Shape = new EquipmentShape();
            this.Sprite = new EquipmentSprite();
            this.EnergySource = new EquipmentEnergySource();
        }
    }
}
