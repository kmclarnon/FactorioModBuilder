using FactorioModBuilder.Models;
using FactorioModBuilder.Models.ProjectItems;
using FactorioModBuilder.Models.SolutionItems;
using FactorioModBuilder.ViewModels.ProjectItems;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfUtils;

namespace FactorioModBuilder.ViewModels
{
    public class SolutionVM : TreeItemVM<Solution, SolutionVM>
    {      
        public SolutionVM(Solution sol, IEnumerable<ProjectVM> projects)
            : base(sol, projects)
        {
            this.Children.CollectionChanged += Children_CollectionChanged;
            this.InitComplete();
        }

        void Children_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.NotifyPropertyChanged("Name");
        }

        protected override string OnGetName(string name)
        {
            var res = "Solution '" + name + "' (" + this.Children.Count + " ";
            if (this.Children.Count == 1)
                res += " project)";
            else
                res += " projects)";
            return res;
        }
    }
}
