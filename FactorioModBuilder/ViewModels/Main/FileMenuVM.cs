using FactorioModBuilder.Models.Dialogs;
using FactorioModBuilder.Models.ProjectItems;
using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.Models.SolutionItems;
using FactorioModBuilder.View.Dialogs;
using FactorioModBuilder.ViewModels.Base;
using FactorioModBuilder.ViewModels.ProjectItems;
using FactorioModBuilder.ViewModels.ProjectItems.Prototype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfUtils;

namespace FactorioModBuilder.ViewModels.Main
{
    public class FileMenuVM : BaseVM
    {
        public ICommand NewProjectCmd { get { return this.GetCommand(this.NewProject, this.CanNewProject); } }
        public ICommand OpenSolutionCmd { get { return this.GetCommand(this.OpenSolution, this.CanOpenSolution); } }
        public ICommand SaveSolutionCmd { get { return this.GetCommand(this.SaveSolution, this.CanSaveSolution); } }
        public ICommand SaveSolutionAsCmd { get { return this.GetCommand(this.SaveSolutionAs, this.CanSaveSolutionAs); } }
        public ICommand CloseSolutionCmd { get { return this.GetCommand(this.CloseSolution, this.CanCloseSolution); } }
        public ICommand ExitCmd { get { return this.GetCommand(this.Exit, this.CanExit); } }

        private MainVM _parent;

        public FileMenuVM(MainVM parent)
        {
            _parent = parent;
        }

        private bool CanNewProject()
        {
            return true;
        }

        private void NewProject()
        {
            var npw = new NewProjectDialog();
            npw.Owner = App.Current.MainWindow;
            npw.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;

            if (npw.ShowDialog() == true)
            {
                var result = npw.NewProjectResult;
                switch (result.ResultSolutionType)
                {
                    case SolutionType.CreateNew:
                        _parent.SolutionExplorer.Solutions.Clear();
                        var vm = _parent.CreateNewSolution(result.ResultSolutionName, 
                            result.ResultProjectName, result.ResultLocation);
                        vm.ExpandDown();
                        _parent.SolutionExplorer.Solutions.Add(vm);
                        break;
                    case SolutionType.AddExisting:
                        break;
                    case SolutionType.CreateInNewInstance:
                        _parent.CreateInNewInstance(result.ResultSolutionName, result.ResultProjectName, result.ResultLocation);
                        break;
                    default:
                        throw new ArgumentException("Unknown Solution Type");
                }
            }
        }

        private bool CanOpenSolution()
        {
            return true;
        }

        private void OpenSolution()
        {
        }

        private bool CanSaveSolution()
        {
            return false;
        }

        private void SaveSolution()
        {
        }

        private bool CanSaveSolutionAs()
        {
            return false;
        }

        private void SaveSolutionAs()
        {

        }

        private bool CanCloseSolution()
        {
            return false;
        }

        private void CloseSolution()
        {

        }

        private bool CanExit()
        {
            return true;
        }

        private void Exit()
        {
            
        }


    }
}
