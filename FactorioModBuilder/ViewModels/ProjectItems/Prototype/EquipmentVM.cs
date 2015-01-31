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
        public string Type
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The energy input parameter of the Equipment model
        /// </summary>
        public int EnergyInput
        {
            get { return this.GetProperty<int>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The shape width of the Equipment model
        /// </summary>
        public int ShapeWidth
        {
            get { return this.GetProperty<int>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The shape height of the Equipment model
        /// </summary>
        public int ShapeHeight
        {
            get { return this.GetProperty<int>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The file path of the Equipment model sprite
        /// </summary>
        public string SpriteFilename
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The width of the Equipment model sprite
        /// </summary>
        public int SpriteWidth
        {
            get { return this.GetProperty<int>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The height of the Equipment model sprite
        /// </summary>
        public int SpriteHeight
        {
            get { return this.GetProperty<int>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The priority of the Equipment model sprite
        /// </summary>
        public SpritePriority SpritePriority
        {
            get { return this.GetProperty<SpritePriority>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The shape type of the Equipment model
        /// </summary>
        public ShapeType ShapeType
        {
            get { return this.GetProperty<ShapeType>(); }
            set { this.SetProperty(value); }
        }

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
        /// The energy source type of the Equipment model
        /// </summary>
        public string EnergySourceType
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The buffer capacity of the Equipment model
        /// </summary>
        public string BufferCap
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The input limit value of the Equipment model
        /// </summary>
        public string InputLimit
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The output limit value of the Equipment model
        /// </summary>
        public string OutputLimit
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The usage priority value of the Equipment model
        /// </summary>
        public string UsagePriority
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The basic constructor to wrap an Equipment model in a view model
        /// </summary>
        /// <param name="equip"></param>
        public EquipmentVM(Equipment equip)
            : base(equip, DoubleClickBehavior.OpenContent)
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
        }
    }
}
