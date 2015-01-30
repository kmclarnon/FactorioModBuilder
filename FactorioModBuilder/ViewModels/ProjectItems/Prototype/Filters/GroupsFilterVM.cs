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
        private int _newCount = 1;

        public GroupsFilterVM(string name)
            : base(name)
        {
            this.MenuItems.Add(new CategoryItem("Add",
                new ClickableItem("New Group", this.AddNewGroupVM),
                new ClickableItem("New Filter", this.AddFilter)));
        }

        private void AddNewGroupVM()
        {
            this.AddChild(new GroupVM(new Group("New Group " + _newCount)));
            _newCount++;
        }
    }
}
