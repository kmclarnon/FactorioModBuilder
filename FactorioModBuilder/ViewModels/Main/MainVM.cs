using FactorioModBuilder.ViewModels.ProjectItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfUtils;
using FactorioModBuilder.Models;
using System.Windows.Input;
using FactorioModBuilder.View;
using FactorioModBuilder.Models.SolutionItems;
using System.Collections.ObjectModel;
using FactorioModBuilder.Models.ProjectItems;
using FactorioModBuilder.ViewModels.Base;
using FactorioModBuilder.ViewModels.ProjectItems.Prototype;
using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.View.Dialogs;
using FactorioModBuilder.Models.Dialogs;
using FactorioModBuilder.Models.Main;
using FactorioModBuilder.ViewModels.Main;
using FactorioModBuilder.Build;
using System.IO;
using System.Diagnostics;
using FactorioModBuilder.ViewModels.Menu.Base;
using FactorioModBuilder.ViewModels.Menu;

namespace FactorioModBuilder.ViewModels
{
    public class MainVM : BaseVM
    {
        public string AppTitle { get { return _main.AppTitle; } }
        public string AppName { get { return _main.AppName; } }
        public string AppVersion { get { return _main.AppVersion; } }
        
        public int AppHeight
        {
            get { return _main.AppHeight; }
            set { this.SetProperty(_main, value); }
        }

        public int AppWidth
        {
            get { return _main.AppWidth; }
            set { this.SetProperty(_main, value); }
        }

        public int AppTop
        {
            get { return _main.AppTop; }
            set { this.SetProperty(_main, value); }
        }

        public int AppLeft
        {
            get { return _main.AppLeft; }
            set { this.SetProperty(_main, value); }
        }

        public SolutionExplorerVM SolutionExplorer { get; private set; }

        public MainMenuVM MainMenu { get; private set; }

        public MainContentVM MainContent { get; private set; }

        public Compiler Compiler { get { return _main.Compiler; } }

        private MainModel _main;

        public MainVM(MainModel m)
        {
            _main = m;
            this.SolutionExplorer = new SolutionExplorerVM();
            this.MainMenu = new MainMenuVM();
            this.MainContent = new MainContentVM();
        }

        public SolutionVM CreateNewSolution(string solutionName, string projectName, string location)
        {
            return new SolutionVM(
                new Solution(solutionName, projectName),
                new List<ProjectVM>() 
                { 
                    new ProjectVM(new Project(projectName, 
                        Path.Combine(location, projectName, "temp"),
                        Path.Combine(location, projectName, "output")),
                        new List<TreeItemVMBase>()
                        {
                            new ModInfoVM(new ModInfo(projectName)),
                            new ModControlVM(new ModControl()),
                            new GraphicsVM(new Graphics()),
                            new PrototypesVM(new Prototypes(),
                                new List<TreeItemVMBase>()
                                {
                                    new GroupsVM(new Groups()),
                                    new SubGroupsVM(new SubGroups()),
                                    new EntitiesVM(new Entities()),
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
        }

        public void CreateAndLoadNewSolution(string solutionName, string projectName, string location)
        {
            this.SolutionExplorer.Solutions.Clear();
            var vm = this.CreateNewSolution(this.Unwrap(solutionName), this.Unwrap(projectName), location);
            vm.ExpandDown();
            this.SolutionExplorer.Solutions.Add(vm);
        }

        public void CreateInNewInstance(string solutionName, string projectName, string location)
        {
            ProcessStartInfo pInfo = new ProcessStartInfo();
            pInfo.Arguments = "-create " + this.Wrap(solutionName) + " " + this.Wrap(projectName) + " " + location;
            pInfo.FileName = System.Reflection.Assembly.GetEntryAssembly().CodeBase;

            try
            {
                Process.Start(pInfo);
            }
            catch(Exception)
            {
                // log this error
            }
        }

        private string Wrap(string val)
        {
            if (!val.Any(o => Char.IsWhiteSpace(o)))
                return val;
            if (!val.StartsWith("\""))
                val = "\"" + val;
            if (!val.EndsWith("\""))
                val = val + "\"";
            return val;
        }

        private string Unwrap(string val)
        {
            if (val.Any(o => Char.IsWhiteSpace(o)))
                return val;
            if (val.StartsWith("\"") && val.Length > 1)
                val = val.Substring(1);
            if (val.EndsWith("\""))
                val = val.Substring(0, val.Length - 1);
            return val;
        }
    }
}
