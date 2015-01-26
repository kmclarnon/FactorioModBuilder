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

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    /// <summary>
    /// The view model used to wrap the solution model
    /// </summary>
    public class SolutionVM : TreeItemVM<Solution, SolutionVM> 
    {   
        /// <summary>
        /// The constructor used to wrap the given Solution model in a view model and associate
        /// it with the given project children.
        /// </summary>
        /// <param name="sol"></param>
        /// <param name="projects"></param>
        public SolutionVM(Solution sol, IEnumerable<ProjectVM> projects)
            : base(sol, projects)
        {
            this.Children.CollectionChanged += Children_CollectionChanged;
            // As the SolutionVM is the top level of the TreeView, it is appropriate to call Init complete
            // at this point since there is no further construction to perform
            this.InitComplete();
        }

        /// <summary>
        /// Handles the events thrown from the addition or removal of children (projects) to this solution view model
        /// </summary>
        private void Children_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.NotifyPropertyChanged("Name");
        }

        /// <summary>
        /// Modifies the solution name to from "name" to "Solution 'name' (# projects)"
        /// </summary>
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
