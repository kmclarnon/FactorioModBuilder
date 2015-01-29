using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype.Filters
{
    public class GroupsFilterVM : FilterBaseVM<GroupVM>
    {
        public GroupsFilterVM(string name)
            : base(name)
        {
        }

        public GroupsFilterVM(TreeItemVMBase parent, string name)
            : base(parent, name)
        {
        }
    }
}
