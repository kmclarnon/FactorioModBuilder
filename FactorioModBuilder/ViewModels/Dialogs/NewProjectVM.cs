using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfUtils;
using FactorioModBuilder.Models;
using FactorioModBuilder.Models.Dialogs;
using System.IO;

namespace FactorioModBuilder.ViewModels.Dialogs
{
    /// <summary>
    /// The view model for the New Project Dialog
    /// </summary>
    public class NewProjectVM : BaseVM
    {
        public ICommand OkCmd { get { return this.GetCommand(this.OK); } }
        public ICommand CancelCmd { get { return this.GetCommand(this.Cancel); } }
        public ICommand BrowseLocationCmd { get { return this.GetCommand(this.BrowseLocation); } }

        public string ResultProjectName
        {
            get { return this.Project.ResultProjectName; }
            set { this.SetProperty(value, false, null, (x => this.OnUpdateName())); }
        }

        public string ResultLocation
        {
            get { return this.Project.ResultLocation; }
            set { this.SetProperty(this.Project, value); }
        }

        public SolutionType ResultSolutionType
        {
            get { return this.Project.ResultSolutionType; }
            set { this.SetProperty(value); }
        }

        public string ResultSolutionName
        {
            get { return this.Project.ResultSolutionName; }
            set { this.SetProperty(value, false, (x => this.OnUpdateSolution())); }
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

        public string Message
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
        }

        private Action<bool> _setResult;
        private bool _solutionModified = false;

        public NewProject Project { get; private set; }


        public NewProjectVM(Action<bool> setResult)
        {
            _setResult = setResult;
            this.Project = new NewProject();
            this.RegisterModel(this.Project);
        }

        private bool CanOK()
        {
            try
            {
                if (this.ResultProjectName == String.Empty || this.ResultProjectName == null)
                {
                    this.Message = "A valid project name is required";
                    return false;
                }
                if (this.ResultSolutionName == String.Empty || this.ResultSolutionName == null)
                {
                    this.Message = "A valid solution name is required";
                    return false;
                }
                if (this.ResultLocation == null || this.ResultLocation == String.Empty)
                {
                    this.Message = "A valid location path is required";
                    return false;
                }
                var p = Path.GetFullPath(this.ResultLocation);
                this.Message = String.Empty;
                return true;
            }
            catch(Exception)
            {
                this.Message = "A valid location path is required";
                return false;
            }
        }

        private void OK()
        {
            if(this.CanOK())
                _setResult(true);
        }

        private void Cancel()
        {
            _setResult(false);
        }

        private void BrowseLocation()
        {
            var dlg = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            if(dlg.ShowDialog() == true)
            {
                this.ResultLocation = dlg.SelectedPath;
            }
        }

        private void OnUpdateName()
        {
            if (!_solutionModified)
            {
                this.Project.ResultSolutionName = this.ResultProjectName;
                this.NotifyPropertyChanged("ResultSolutionName");
            }
        }

        private void OnUpdateSolution()
        {
            _solutionModified = true;
            if (this.ResultSolutionName == String.Empty)
                _solutionModified = false;
        }
    }
}
