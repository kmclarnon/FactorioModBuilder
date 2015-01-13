using FactorioModBuilder.Build;
using FactorioModBuilder.Build.Data;
using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
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
    public class GroupVM : ProjectItem<Group, GroupVM>
    {
        public override DataUnit CompilerData
        {
            get
            {
                return new GroupData(this.Type, 
                    this.IconPath, this.InvOrder, this.Order);
            }
        }

        public string Type
        {
            get { return _internal.Type; }
            set { this.SetProperty(_internal, value); }
        }

        public string IconPath
        {
            get { return _internal.IconPath; }
            set { this.SetProperty(_internal, value); }
        }

        public string InvOrder
        {
            get { return _internal.InvOrder; }
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

        public ICommand FindImageCmd { get { return this.GetCommand(this.FindImage, this.CanFindImage); } }

        public GroupVM(Group group)
            : base(group)
        {
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
