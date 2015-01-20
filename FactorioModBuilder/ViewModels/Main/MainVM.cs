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

        public ObservableCollection<SolutionVM> Solutions { get; private set; }

        public FileMenuVM FileMenu { get; private set; }
        public BuildMenuVM BuildMenu { get; private set; }
        public Compiler Compiler { get { return _main.Compiler; } }

        private MainModel _main;

        public MainVM(MainModel m)
        {
            _main = m;
            this.Solutions = new ObservableCollection<SolutionVM>();
            this.FileMenu = new FileMenuVM(this);
            this.BuildMenu = new BuildMenuVM(this);
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
                            new ModDataVM(new ModData()),
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
            this.Solutions.Clear();
            var vm = this.CreateNewSolution(this.Unwrap(solutionName), this.Unwrap(projectName), location);
            vm.ExpandDown();
            this.Solutions.Add(vm);
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
