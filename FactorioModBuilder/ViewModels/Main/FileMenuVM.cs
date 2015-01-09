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
    public class FileMenuVM
    {
        private ICommand _newProjectCmd;
        public ICommand NewProjectCmd
        {
            get
            {
                if (_newProjectCmd == null)
                    _newProjectCmd = new RelayCommand(
                        (x => this.NewProject()), (x => this.CanNewProject()));
                return _newProjectCmd;
            }
        }

        private ICommand _openSolutionCmd;
        public ICommand OpenSolutionCmd
        {
            get
            {
                if (_openSolutionCmd == null)
                    _openSolutionCmd = new RelayCommand(
                        (x => this.OpenSolution()), (x => this.CanOpenSolution()));
                return _openSolutionCmd;
            }
        }

        private ICommand _saveSolutionCmd;
        public ICommand SaveSolutionCmd
        {
            get
            {
                if (_saveSolutionCmd == null)
                    _saveSolutionCmd = new RelayCommand(
                        (x => this.SaveSolution()), (x => this.CanSaveSolution()));
                return _saveSolutionCmd;
            }
        }

        private ICommand _saveSolutionAsCmd;
        public ICommand SaveSolutionAsCmd
        {
            get
            {
                if (_saveSolutionAsCmd == null)
                    _saveSolutionAsCmd = new RelayCommand(
                        (x => this.SaveSolutionAs()), (x => this.CanSaveSolutionAs()));
                return _saveSolutionAsCmd;
            }
        }

        private ICommand _closeSolutionCmd;
        public ICommand CloseSolutionCmd
        {
            get
            {
                if (_closeSolutionCmd == null)
                    _closeSolutionCmd = new RelayCommand(
                        (x => this.CloseSolution()), (x => this.CanCloseSolution()));
                return _closeSolutionCmd;
            }
        }

        private ICommand _exitCmd;
        public ICommand ExitCmd
        {
            get
            {
                if (_exitCmd == null)
                    _exitCmd = new RelayCommand(
                        (x => this.Exit()), (x => this.CanExit()));
                return _exitCmd;
            }
        }

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
                        _parent.Solutions.Clear();
                        var vm = new SolutionVM(
                            new Solution(result.ResultSolutionName, result.ResultLocation),
                            new List<ProjectVM>() 
                            { 
                                    new List<TreeItemVMBase>()
                                    {
                                        new ModInfoVM(new ModInfo(result.ResultProjectName)),
                                        new ModDataVM(new ModData()),
                                        new ModControlVM(new ModControl()),
                                        new PrototypesVM(new Prototypes(),
                                            new List<TreeItemVMBase>()
                                            {
                                                new GroupsVM(new Groups()),
                                                new SubGroupsVM(new SubGroups()),
                                                new EquipsVM(new Equips()),
                                                new FluidsVM(new Fluids()),
                                                new ItemsVM(new Items()),
                                                new RecipesVM(new Recipes()),
                                                new TechnologiesVM(new Technologies()),
                                                new TilesVM(new Tiles())
                                            }),
                                        new LocaleVM(new Locale())
                                    }),
                            });
                        vm.ExpandDown();
                        _parent.Solutions.Add(vm);
                        break;
                    case SolutionType.AddExisting:
                        break;
                    case SolutionType.CreateInNewInstance:
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
            return _parent.Solutions.Count > 0;
        }

        private void SaveSolution()
        {
        }

        private bool CanSaveSolutionAs()
        {
            return _parent.Solutions.Count > 0;
        }

        private void SaveSolutionAs()
        {

        }

        private bool CanCloseSolution()
        {
            return _parent.Solutions.Count > 0;
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
