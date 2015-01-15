using FactorioModBuilder.Build.Data;
using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using FactorioModBuilder.Extensions;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfUtils;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class ItemVM : ProjectItem<Item, ItemVM>
    {
        public override IEnumerable<DataUnit> CompilerData
        {
            get 
            { 
                return new ItemData(this.Name, this.IconPath, 
                this.SubGroupName, this.Order, 
                this.PlaceResult, this.StackSize).ListWrap(); 
            }
        }

        public SubGroupVM SubGroup
        {
            get { return this.GetProperty<SubGroupVM>(); }
            set { this.SetProperty(value, (() => this.SubGroupName = (value == null) ? String.Empty : value.Name)); }
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

        public bool Enabled
        {
            get { return _internal.Enabled; }
            set { this.SetProperty(_internal, value); }
        }

        public string IconPath
        {
            get { return _internal.IconPath; }
            set { this.SetProperty(_internal, value); }
        }

        public int StackSize
        {
            get { return _internal.StackSize; }
            set { this.SetProperty(_internal, value); }
        }

        public string PlaceResult
        {
            get { return _internal.PlaceResult; }
            set { this.SetProperty(_internal, value); }
        }

        public ICommand FindImageCmd { get { return this.GetCommand(this.FindImage, this.CanFindImage); } }

        public ItemVM(Item item)
            : base(item)
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
    }
}
