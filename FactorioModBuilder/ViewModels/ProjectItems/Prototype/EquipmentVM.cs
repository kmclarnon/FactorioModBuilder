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
    public class EquipmentVM : TreeItemVM<Equipment, EquipmentVM>
    {
        /// <summary>
        /// The type parameter of the Equipment model
        /// </summary>
        public string Type
        {
            get { return _internal.Type; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The energy input parameter of the Equipment model
        /// </summary>
        public int EnergyInput
        {
            get { return _internal.EnergyInput; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The shape width of the Equipment model
        /// </summary>
        public int ShapeWidth
        {
            get { return _internal.ShapeWidth; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The shape height of the Equipment model
        /// </summary>
        public int ShapeHeight
        {
            get { return _internal.ShapeHeight; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The file path of the Equipment model sprite
        /// </summary>
        public string SpriteFilename
        {
            get { return _internal.SpriteFilename; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The width of the Equipment model sprite
        /// </summary>
        public int SpriteWidth
        {
            get { return _internal.SpriteWidth; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The height of the Equipment model sprite
        /// </summary>
        public int SpriteHeight
        {
            get { return _internal.SpriteHeight; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The priority of the Equipment model sprite
        /// </summary>
        public string SpritePriority
        {
            get { return _internal.SpritePriority; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The shape type of the Equipment model
        /// </summary>
        public string ShapeType
        {
            get { return _internal.ShapeType; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The max shield value of the Equipment model
        /// </summary>
        public int MaxShieldValue
        {
            get { return _internal.MaxShieldValue; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The energy per shield value of the Equipment model
        /// </summary>
        public int EnergyPerShield
        {
            get { return _internal.EnergyPerShield; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The energy source type of the Equipment model
        /// </summary>
        public string EnergySourceType
        {
            get { return _internal.EnergySourceType; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The buffer capacity of the Equipment model
        /// </summary>
        public string BufferCap
        {
            get { return _internal.BufferCap; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The input limit value of the Equipment model
        /// </summary>
        public string InputLimit
        {
            get { return _internal.InputLimit; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The output limit value of the Equipment model
        /// </summary>
        public string OutputLimit
        {
            get { return _internal.OutputLimit; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The usage priority value of the Equipment model
        /// </summary>
        public string UsagePriority
        {
            get { return _internal.UsagePriority; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The basic constructor to wrap an Equipment model in a view model
        /// </summary>
        /// <param name="equip"></param>
        public EquipmentVM(Equipment equip)
            : base(equip)
        {
        }

        /// <summary>
        /// The constructor to wrap an Equipment model in a view model and associate it with
        /// the given parent
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="equip"></param>
        public EquipmentVM(TreeItemVMBase parent, Equipment equip)
            : base(parent, equip)
        {
        }
    }
}
