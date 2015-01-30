using FactorioModBuilder.ViewModels.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype.Filters
{
    public class SubGroupFilterVM : FilterBaseVM<SubGroupVM>
    {
        private int _newCount = 1;

        public SubGroupFilterVM(string name)
            : base(name)
        {
            this.MenuItems.Add(new CategoryItem("Add",
                new ClickableItem("New SubGroup", this.AddNewSubGroupVM),
                new ClickableItem("New Filter", this.AddFilter)));
        }

        private void AddNewSubGroupVM()
        {
            this.AddChild(new SubGroupVM(new Models.ProjectItems.Prototype.SubGroup("New SubGroup " + _newCount)));
            _newCount++;
        }
    }
}
