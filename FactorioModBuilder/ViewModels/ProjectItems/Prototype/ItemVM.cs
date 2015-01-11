using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
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
    public class ItemVM : TreeItemVM<Item, ItemVM>
    {
        public string Type { get { return _internal.Type; } }

        public string Subgroup
        {
            get { return _internal.Subgroup; }
            set { this.SetProperty(_internal, value); }
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

        private ICommand _findImageCmd;
        public ICommand FindImageCmd
        {
            get
            {
                if (_findImageCmd == null)
                    _findImageCmd = new RelayCommand((x => this.FindImage()),
                        (x => this.CanFindImage()));
                return _findImageCmd;
            }
        }

        public ObservableCollection<SubGroupVM> PossibleSubgroups
        {
            get
            {
                TreeItemVMBase res;
                if (!this.TryFindElementWithPropertyUp(typeof(ObservableCollection<SubGroupVM>),
                    "PossibleSubgroups", out res))
                {
                    throw new Exception("Failed to find parent to supply Possible Subgroups");
                }
                return (ObservableCollection<SubGroupVM>)res.GetType()
                    .GetProperty("PossibleSubgroups").GetValue(res);
            }
        }

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
