using FactorioModBuilder.Build.Data;
using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using FactorioModBuilder.Extensions;
using FactorioModBuilder.ViewModels.ProjectItems.Prototype;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfUtils;
using System.Text.RegularExpressions;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class ItemVM : ProjectItem<Item, ItemVM>, IGraphicsSource
    {
        public override IEnumerable<DataUnit> CompilerData
        {
            get 
            { 
                return new ItemData(this.Name, this.IconPath, 
                    this.SubGroupName, this.Order, 
                    this.PlaceResult, this.StackSize, this.Flag).ListWrap(); 
            }
        }

        public string GraphicPath
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
        }

        public SubGroupVM SubGroup
        {
            get { return this.GetProperty<SubGroupVM>(); }
            set 
            { 
                this.SetProperty(value, 
                    this.UpdateSubGroupBinding, 
                    (() => this.SubGroupName = (value == null) ? String.Empty : value.Name)); 
            }
        }

        public string SubGroupName
        {
            get { return _internal.SubGroup; }
            set { this.SetProperty(_internal, value, false, null, "SubGroup"); }
        }

        public string Order
        {
            get { return _internal.Order; }
            set { this.SetProperty(_internal, value); }
        }

        public string IconPath
        {
            get { return _internal.IconPath; }
            set { this.SetProperty(_internal, value, false, (() => this.GraphicPath = (value == null) ? String.Empty : value)); }
        }

        public int StackSize
        {
            get { return _internal.StackSize; }
            set { this.SetProperty(_internal, value); }
        }

        public EntityVM PlaceResultEntity
        {
            get { return this.GetProperty<EntityVM>(); }
            set { this.SetProperty(value, null, (() => this.PlaceResult = (value == null) ? String.Empty : value.Name)); }
        }

        public string PlaceResult
        {
            get { return _internal.PlaceResult; }
            set { this.SetProperty(_internal, value); }
        }

        public ItemFlag Flag
        {
            get { return _internal.Flag; }
            set { this.SetProperty(_internal, value); }
        }

        public ICommand FindImageCmd { get { return this.GetCommand(this.FindImage, this.CanFindImage); } }

        static ItemVM()
        {
            ItemVM.AddPropertyValidation("StackSize",
                (x => Regex.IsMatch(x.StackSize.ToString(), @"^\d+$")),
                "Stacksize must be a positive whole number");
        }

        public ItemVM(Item item)
            : base(item, DoubleClickBehavior.OpenContent)
        {
        }

        public ItemVM(TreeItemVMBase parent, Item item)
            : base(parent, item)
        {
        }

        private bool CanFindImage()
        {
            return true;
        }

        private void FindImage()
        {
            var ofd = new OpenFileDialog();
            ofd.CheckFileExists = true;
            ofd.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            ofd.Multiselect = false;

            if(ofd.ShowDialog() == true)
            {
                this.IconPath = ofd.FileName;
            }
        }

        /// <summary>
        /// Handles hooking and unhooking the SubGroupVM's property changed notification to catch renaming
        /// </summary>
        /// <param name="val"></param>
        private void UpdateSubGroupBinding(SubGroupVM val)
        {
            if (this.SubGroup != null)
                this.SubGroup.PropertyChanged -= SubGroupPropertyChanged;
            if (val != null)
                val.PropertyChanged += SubGroupPropertyChanged;
        }

        /// <summary>
        /// Re-reads the name property from the subgroup whenever there is a property change
        /// </summary>
        void SubGroupPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (this.SubGroup == null)
                this.SubGroupName = String.Empty;
            else
                this.SubGroupName = this.SubGroup.Name;
        }
    }
}
