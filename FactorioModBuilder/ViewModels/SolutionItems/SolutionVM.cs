using FactorioModBuilder.Models;
using FactorioModBuilder.ViewModels.ProjectItems;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfUtils;

namespace FactorioModBuilder.ViewModels
{
    public class SolutionVM : BaseVM
    {
        public ObservableCollection<ProjectVM> Projects { get; private set; }
        
        public string Name
        {
            get { return _sol.Name; }
            set
            {
                if(_sol.Name != value)
                {
                    _sol.Name = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        private Solution _sol;

        public SolutionVM(Solution sol)
        {
            this.Projects = new ObservableCollection<ProjectVM>();
            _sol = sol;
            foreach (var p in _sol.Projects)
                this.Projects.Add(new ProjectVM(p));
        }
    }
}
