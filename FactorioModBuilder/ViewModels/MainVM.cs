using FactorioModBuilder.ViewModels.ProjectItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfUtils;
using FactorioModBuilder.Models;

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
            set
            {
                if(_main.AppHeight != value)
                {
                    _main.AppHeight = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public int AppWidth
        {
            get { return _main.AppWidth; }
            set
            {
                if(_main.AppWidth != value)
                {
                    _main.AppWidth = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        private ProjectVM _activeProject;
        public ProjectVM ActiveProject
        {
            get { return _activeProject; }
            set
            {
                if(_activeProject != value)
                {
                    _activeProject = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        private Main _main;

        public MainVM(Main m)
        {
            _main = m;
            // test
            this.ActiveProject = new ProjectVM(new Project());
            foreach (var i in this.ActiveProject.ProjectItems)
                i.ExpandDown();
        }
    }
}
