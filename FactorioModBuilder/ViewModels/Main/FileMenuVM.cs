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
