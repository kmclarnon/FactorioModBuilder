using FactorioModBuilder.Build;
using FactorioModBuilder.Build.Data;
using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using FactorioModBuilder.Extensions;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfUtils;
using FactorioModBuilder.Extensions;
using System.Collections.ObjectModel;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class GroupVM : ProjectItem<Group, GroupVM>, IGraphicsSource
    {
        public override IEnumerable<DataUnit> CompilerData
        {
            get
            {
                return new GroupData(this.Name, 
                    this.IconPath, this.InvOrder, this.Order).ListWrap();
            }
        }

        public string GraphicPath
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
        }

        public string Type
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
        }

        public string IconPath
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value, false, null, (x => this.GraphicPath = (value == null) ? String.Empty : value)); }
        }

        public string InvOrder
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
        }

        public string Order
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
        }

        public bool Enabled
        {
            get { return this.GetProperty<bool>(); }
            set { this.SetProperty(value); }
        }

        public ICommand FindImageCmd { get { return this.GetCommand(this.FindImage, this.CanFindImage); } }

        public ICommand AddSubGroupCmd { get { return this.GetCommand(this.AddSubGroup); } }

        public ICommand RemoveSubGroupCmd { get { return this.GetCommand(this.RemoveSubGroup, this.CanRemoveSubGroup); } }

        private int _newCount = 1;

        public GroupVM(Group group)
            : this(null, group)
        {
        }

        public GroupVM(TreeItemVMBase parent, Group group)
            : base(parent, group, DoubleClickBehavior.OpenContent)
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

        private void AddSubGroup()
        {
            this.Children.Add(new SubGroupVM(new SubGroup("new-subgroup-" + _newCount)));
            _newCount++;
        }

        private bool CanRemoveSubGroup()
        {
            return this.Children.Any(o => o.IsSelected);
        }

        private void RemoveSubGroup()
        {
            this.Children.RemoveWhere(o => o.IsSelected);
        }
    }
}
