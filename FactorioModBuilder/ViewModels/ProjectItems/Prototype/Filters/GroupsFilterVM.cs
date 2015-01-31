using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using FactorioModBuilder.ViewModels.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype.Filters
{
    /// <summary>
    /// A filter for Group prototypes
    /// </summary>
    public class GroupsFilterVM : FilterBaseVM<GroupVM>
    {
        /// <summary>
        /// Counter to keep track of new groups created
        /// </summary>
        private int _newChild = 1;

        public GroupsFilterVM(string name)
            : base(name, "Group")
        {
        }

        protected override FilterBaseVM<GroupVM> GetNewFilter()
        {
            return new GroupsFilterVM("New Filter");
        }

        protected override GroupVM GetNewChild()
        {
            return new GroupVM(new Group("New Group " + _newChild++));
        }
    }
}
