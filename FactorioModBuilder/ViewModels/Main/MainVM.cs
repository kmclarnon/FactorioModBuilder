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
    }
}
