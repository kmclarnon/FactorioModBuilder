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
        public ObservableCollection<SubGroupVM> SubgroupList { get; private set; }

        public SubGroupsVM(ProjectItemVM parent, SubGroups items)
            : base(parent, items)
        {
            this.SubgroupList = new ObservableCollection<SubGroupVM>();
        }
    }
}
