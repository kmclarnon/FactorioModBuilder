using FactorioModBuilder.Models.ProjectItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class GroupsVM : ProjectItemVM
    {
        public GroupsVM(ProjectItemVM parent, Groups groups)
            : base(parent, groups)
        { 
        }
    }
}
