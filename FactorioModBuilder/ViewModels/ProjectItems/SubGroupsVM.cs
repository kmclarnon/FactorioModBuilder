using FactorioModBuilder.Models.ProjectItems;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class SubGroupsVM : ProjectItemVM
    {
        public ObservableCollection<String> SubgroupNames { get; set; }

        public SubGroupsVM(ProjectItemVM parent, SubGroups items)
            : base(parent, items)
        {
            this.SubgroupNames = new ObservableCollection<string>();
            this.SubgroupNames.Add("Sg 1");
            this.SubgroupNames.Add("Sg 2");
        }
    }
}
