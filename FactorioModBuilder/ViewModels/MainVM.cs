using FactorioModBuilder.ViewModels.ProjectItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfUtils;

namespace FactorioModBuilder.ViewModels
{
    public class MainVM : BaseVM
    {
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

        public MainVM()
        {
            // test
            this.ActiveProject = new ProjectVM(new Project());
        }
    }
}
