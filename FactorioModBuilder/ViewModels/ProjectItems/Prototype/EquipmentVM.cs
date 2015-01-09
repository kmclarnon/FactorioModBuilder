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
            set
            {
                if(_internal.Type != value)
                {
                    _internal.Type = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public int EnergyInput
        {
            get { return _internal.EnergyInput; }
            set
            {
                if(_internal.EnergyInput != value)
                {
                    _internal.EnergyInput = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public int ShapeWidth
        {
            get { return _internal.ShapeWidth; }
            set
            {
                if(_internal.ShapeWidth != value)
                {
                    _internal.ShapeWidth = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public int ShapeHeight
        {
            get { return _internal.ShapeHeight; }
            set
            {
                if(_internal.ShapeHeight != value)
                {
                    _internal.ShapeHeight = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public string SpriteFilename
        {
            get { return _internal.SpriteFilename; }
            set
            {
                if(_internal.SpriteFilename != value)
                {
                    _internal.SpriteFilename = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public int SpriteWidth
        {
            get { return _internal.SpriteWidth; }
            set
            {
                if(_internal.SpriteWidth != value)
                {
                    _internal.SpriteWidth = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public int SpriteHeight
        {
            get { return _internal.SpriteHeight; }
            set
            {
                if(_internal.SpriteHeight != value)
                {
                    _internal.SpriteHeight = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public string SpritePriority
        {
            get { return _internal.SpritePriority; }
            set
            {
                if(_internal.SpritePriority != value)
                {
                    _internal.SpritePriority = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public string ShapeType
        {
            get { return _internal.ShapeType; }
            set
            {
                if(_internal.ShapeType != value)
                {
                    _internal.ShapeType = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public int MaxShieldValue
        {
            get { return _internal.MaxShieldValue; }
            set
            {
                if(_internal.MaxShieldValue != value)
                {
                    _internal.MaxShieldValue = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public int EnergyPerShield
        {
            get { return _internal.EnergyPerShield; }
            set
            {
                if(_internal.EnergyPerShield != value)
                {
                    _internal.EnergyPerShield = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public string EnergySourceType
        {
            get { return _internal.EnergySourceType; }
            set
            {
                if(_internal.EnergySourceType != value)
                {
                    _internal.EnergySourceType = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public string BufferCap
        {
            get { return _internal.BufferCap; }
            set
            {
                if(_internal.BufferCap != value)
                {
                    _internal.BufferCap = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public string InputLimit
        {
            get { return _internal.InputLimit; }
            set
            {
                if(_internal.InputLimit != value)
                {
                    _internal.InputLimit = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public string OutputLimit
        {
            get { return _internal.OutputLimit; }
            set
            {
                if(_internal.OutputLimit != value)
                {
                    _internal.OutputLimit = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public string UsagePriority
        {
            get { return _internal.UsagePriority; }
            set
            {
                if(_internal.UsagePriority != value)
                {
                    _internal.UsagePriority = value;
                    this.NotifyPropertyChanged();
                }
            }
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
