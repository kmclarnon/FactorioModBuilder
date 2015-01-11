using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class EquipmentVM : TreeItemVM<Equipment, EquipmentVM>
    {
        public string Type
        {
            get { return _internal.Type; }
            set { this.SetProperty(_internal, value); }
        }

        public int EnergyInput
        {
            get { return _internal.EnergyInput; }
            set { this.SetProperty(_internal, value); }
        }

        public int ShapeWidth
        {
            get { return _internal.ShapeWidth; }
            set { this.SetProperty(_internal, value); }
        }

        public int ShapeHeight
        {
            get { return _internal.ShapeHeight; }
            set { this.SetProperty(_internal, value); }
        }

        public string SpriteFilename
        {
            get { return _internal.SpriteFilename; }
            set { this.SetProperty(_internal, value); }
        }

        public int SpriteWidth
        {
            get { return _internal.SpriteWidth; }
            set { this.SetProperty(_internal, value); }
        }

        public int SpriteHeight
        {
            get { return _internal.SpriteHeight; }
            set { this.SetProperty(_internal, value); }
        }

        public string SpritePriority
        {
            get { return _internal.SpritePriority; }
            set { this.SetProperty(_internal, value); }
        }

        public string ShapeType
        {
            get { return _internal.ShapeType; }
            set { this.SetProperty(_internal, value); }
        }

        public int MaxShieldValue
        {
            get { return _internal.MaxShieldValue; }
            set { this.SetProperty(_internal, value); }
        }

        public int EnergyPerShield
        {
            get { return _internal.EnergyPerShield; }
            set { this.SetProperty(_internal, value); }
        }

        public string EnergySourceType
        {
            get { return _internal.EnergySourceType; }
            set { this.SetProperty(_internal, value); }
        }

        public string BufferCap
        {
            get { return _internal.BufferCap; }
            set { this.SetProperty(_internal, value); }
        }

        public string InputLimit
        {
            get { return _internal.InputLimit; }
            set { this.SetProperty(_internal, value); }
        }

        public string OutputLimit
        {
            get { return _internal.OutputLimit; }
            set { this.SetProperty(_internal, value); }
        }

        public string UsagePriority
        {
            get { return _internal.UsagePriority; }
            set { this.SetProperty(_internal, value); }
        }

        public EquipmentVM(Equipment equip)
            : base(equip)
        {
        }

        public EquipmentVM(TreeItemVMBase parent, Equipment equip)
            : base(parent, equip)
        {
        }
    }
}
