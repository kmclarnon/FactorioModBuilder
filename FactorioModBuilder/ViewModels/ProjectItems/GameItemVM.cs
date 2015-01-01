using FactorioModBuilder.Models.ProjectItems;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfUtils;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class GameItemVM : ProjectItemVM
    {
        public string Type { get { return _internal.Type; } }

        public string Subgroup
        {
            get { return _internal.Subgroup; }
            set
            {
                if (_internal.Subgroup != value)
                {
                    _internal.Subgroup = value;
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
                if (_internal.Enabled != value)
                {
                    _internal.Enabled = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public string IconPath
        {
            get { return _internal.IconPath; }
            set
            {
                if (_internal.IconPath != value)
                {
                    _internal.IconPath = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public int StackSize
        {
            get { return _internal.StackSize; }
            set
            {
                if (_internal.StackSize != value)
                {
                    _internal.StackSize = value;
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
                    _findImageCmd = new RelayCommand((x => this.FindImage()),
                        (x => this.CanFindImage()));
                return _findImageCmd;
            }
        }

        public ObservableCollection<SubGroupVM> PossibleSubgroups
        {
            get
            {
                ProjectItemVM res;
                if (!this.TryFindElementWithPropertyUp(typeof(ObservableCollection<SubGroupVM>),
                    "PossibleSubgroups", out res))
                {
                    throw new Exception("Failed to find parent to supply Possible Subgroups");
                }
                return (ObservableCollection<SubGroupVM>)res.GetType()
                    .GetProperty("PossibleSubgroups").GetValue(res);
            }
        }

        private GameItem _internal { get { return (GameItem)_item; } }

        public GameItemVM(ProjectItemVM parent, GameItem item)
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
