using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Utility;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfUtils;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class GroupVM : TreeItemVM<Group>
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

        public string IconPath
        {
            get { return _internal.IconPath; }
            set
            {
                if(_internal.IconPath != value)
                {
                    _internal.IconPath = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public string InvOrder
        {
            get { return _internal.InvOrder; }
            set
            {
                if(_internal.InvOrder != value)
                {
                    _internal.InvOrder = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public string Order
        {
            get { return _internal.Order; }
            set
            {
                if(_internal.Order != value)
                {
                    _internal.Order = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public bool Enabled
        {
            get { return _internal.Enabled; }
            set
            {
                if(_internal.Enabled != value)
                {
                    _internal.Enabled = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        private ICommand _findImageCmd;
        public ICommand FindImageCmd
        {
            get
            {
                if (_findImageCmd == null)
                    _findImageCmd = new RelayCommand(
                        (x => this.FindImage()), (x => this.CanFindImage()));
                return _findImageCmd;
            }
        }

        public GroupVM(TreeItemVMBase parent, Group group)
            : base(parent, group)
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

            if (ofd.ShowDialog() == true)
            {
                this.IconPath = ofd.FileName;
            }
        }
    }
}
