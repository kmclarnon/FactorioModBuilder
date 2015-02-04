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
using System.Windows;
using FactorioModBuilder.ViewModels.ProjectItems.Prototype.Filters;

namespace FactorioModBuilder.ViewModels
{
    public class MainVM : BaseVM
    {
        public string AppTitle { get { return this.GetProperty<string>(); } }
        public string AppName { get { return this.GetProperty<string>(); } }
        public string AppVersion { get { return this.GetProperty<string>(); } }
        
        public int AppHeight
        {
            get { return this.GetProperty<int>(); }
            set { this.SetProperty(value); }
        }

        public int AppWidth
        {
            get { return this.GetProperty<int>(); }
            set { this.SetProperty(value); }
        }

        public int AppTop
        {
            get { return this.GetProperty<int>(); }
            set { this.SetProperty(value); }
        }

        public int AppLeft
        {
            get { return this.GetProperty<int>(); }
            set { this.SetProperty(value); }
        }

        public bool Active
        {
            get { return this.GetProperty<bool>(); }
            set { this.SetProperty(value, false, null, (x => this.ShowBorder = x && (this.WindowState == System.Windows.WindowState.Normal))); }
        }

        public bool ShowBorder
        {
            get { return this.GetProperty<bool>(); }
            set { this.SetProperty(value); }
        }

        public WindowState WindowState
        {
            get { return this.GetProperty<WindowState>(); }
            set { this.SetProperty(value, false, null, (x => this.ShowBorder = this.Active && (this.WindowState == System.Windows.WindowState.Normal))); }
        }

        public SolutionExplorerVM SolutionExplorer { get; private set; }

        public MainMenuVM MainMenu { get; private set; }

        public MainContentVM MainContent { get; private set; }

        public Compiler Compiler { get; private set; }

        public MainVM(MainModel m)
            : base(m)
        {
            this.Compiler = m.Compiler;
            this.SolutionExplorer = new SolutionExplorerVM(this);
            this.MainMenu = new MainMenuVM(this, new FileMenuVM(this));
            this.MainContent = new MainContentVM();
            this.AppTop = 150;
            this.AppLeft = 400;
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
                                    new GroupsFilterVM("Item Groups"),
                                    new ItemFilterVM("Items"),
                                    new EntityFilterVM("Entities"),
                                    new EquipmentFilterVM("Equipment"),
                                    new FluidFilterVM("Fluids")
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

        public void OpenItems(IEnumerable<TreeItemVMBase> items)
        {
            this.MainContent.OpenItems(items);
        }

        public void OpenParentItems(IEnumerable<TreeItemVMBase> items)
        {
            var res = items.Where(o => o.Parent != null).Select(o => o.Parent);
            this.MainContent.OpenItems(res);
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
