using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfUtils;
using FactorioModBuilder.Models;

namespace FactorioModBuilder.ViewModels
{
    public class NewProjectVM : BaseVM
    {
        private ICommand _okCmd;
        public ICommand OkCmd
        {
            get
            {
                if (_okCmd == null)
                    _okCmd = new RelayCommand(
                        (x => this.OK()));
                return _okCmd;
            }
        }

        private ICommand _cancelCmd;
        public ICommand CancelCmd
        {
            get
            {
                if (_cancelCmd == null)
                    _cancelCmd = new RelayCommand(
                        (x => this.Cancel()));
                return _cancelCmd;
            }
        }

        private ICommand _browseLocation;
        public ICommand BrowseLocationCmd
        {
            get
            {
                if (_browseLocation == null)
                    _browseLocation = new RelayCommand(
                        (x => this.BrowseLocation()));
                return _browseLocation;
            }
        }

        public string ResultProjectName
        {
            get { return this.Project.ResultProjectName; }
            set
            {
                if(this.Project.ResultProjectName != value)
                {
                    this.Project.ResultProjectName = value;
                    this.NotifyPropertyChanged();
                    if(!this._solutionModified)
                    {
                        this.Project.ResultSolutionName = value;
                        this.NotifyPropertyChanged("ResultSolutionName");
                    }
                }
            }
        }

        public string ResultLocation
        {
            get { return this.Project.ResultLocation; }
            set
            {
                if(this.Project.ResultLocation != value)
                {
                    this.Project.ResultLocation = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public SolutionType ResultSolutionType
        {
            get { return this.Project.ResultSolutionType; }
            set
            {
                if(this.Project.ResultSolutionType != value)
                {
                    this.Project.ResultSolutionType = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public string ResultSolutionName
        {
            get { return this.Project.ResultSolutionName; }
            set
            {
                if(this.Project.ResultSolutionName != value)
                {
                    _solutionModified = true;
                    this.Project.ResultSolutionName = value;
                    if (value == String.Empty)
                        _solutionModified = false;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public IEnumerable<Tuple<SolutionType, String>> PossibleSolutions
        {
            get
            {
                return new List<Tuple<SolutionType, string>>()
                {
                    new Tuple<SolutionType, string>(
                        SolutionType.CreateNew, "Create New"),
                    new Tuple<SolutionType, string>(
                        SolutionType.AddExisting, "Add Existing"),
                    new Tuple<SolutionType, string>(
                        SolutionType.CreateInNewInstance, "Create In New Instance")
                };
            }
        }

        private Action<bool> _setResult;
        private bool _solutionModified = false;

        public NewProject Project { get; private set; }


        public NewProjectVM(Action<bool> setResult)
        {
            _setResult = setResult;
            this.Project = new NewProject();
        }

        private void OK()
        {
            _setResult(true);
        }

        private void Cancel()
        {
            _setResult(false);
        }

        private void BrowseLocation()
        {
            var dlg = new Gat.Controls.OpenDialogView();
            var vm = (Gat.Controls.OpenDialogViewModel)dlg.DataContext;

            vm.IsDirectoryChooser = true;
            vm.SelectFolder = true;
            vm.FileNameText = "Selected Folder:";

            if (vm.Show() == true)
            {
                this.ResultLocation = vm.SelectedFolder.Path;
            }
        }
    }
}
