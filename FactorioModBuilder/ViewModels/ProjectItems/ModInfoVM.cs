using FactorioModBuilder.Models.ProjectItems;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfUtils;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class ModInfoVM : TreeItemVM<ModInfo>
    {
        public string ModName
        {
            get { return _mItem.ModName; }
            set
            {
                if(_mItem.ModName != value)
                {
                    _mItem.ModName = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public string Version
        {
            get { return _mItem.Version; }
            set
            {
                if(_mItem.Version != value)
                {
                    _mItem.Version = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public string Title
        {
            get { return _mItem.Title; }
            set
            {
                if(_mItem.Title != value)
                {
                    _mItem.Title = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public string Author
        {
            get { return _mItem.Author; }
            set
            {
                if(_mItem.Author != value)
                {
                    _mItem.Author = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public string Contact
        {
            get { return _mItem.Contact; }
            set
            {
                if(_mItem.Contact != value)
                {
                    _mItem.Contact = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public string Homepage
        {
            get { return _mItem.Homepage; }
            set
            {
                _mItem.Homepage = value;
                this.NotifyPropertyChanged();
            }
        }

        public string Description
        {
            get { return _mItem.Description; }
            set
            {
                _mItem.Description = value;
                this.NotifyPropertyChanged();
            }
        }

        private ICommand _cancelCmd;
        public ICommand CancelCmd
        {
            get
            {
                if (_cancelCmd == null)
                {
                    _cancelCmd = new RelayCommand((x => this.DoCancel()),
                        (x => this.CanCancel()));
                }
                return _cancelCmd;
            }
        }

        private ICommand _applyCmd;
        public ICommand ApplyCmd
        {
            get
            {
                if(_applyCmd == null)
                {
                    _applyCmd = new RelayCommand((x => this.DoApply()),
                        (x => this.CanApply()));
                }
                return _applyCmd;
            }
        }

        private ModInfo _mItem { get { return (ModInfo)_item; } }

        public ModInfoVM(ModInfo info)
            : base(info)
        {
        }

        public ModInfoVM(TreeItemVMBase parent, ModInfo info)
            : base(parent, info)
        {
        }

        private bool CanCancel()
        {
            return true;
        }

        private void DoCancel()
        {

        }

        private bool CanApply()
        {
            return true;
        }

        private void DoApply()
        {

        }

    }
}
