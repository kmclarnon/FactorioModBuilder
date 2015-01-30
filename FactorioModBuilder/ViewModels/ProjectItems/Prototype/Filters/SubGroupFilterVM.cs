using FactorioModBuilder.Models.ProjectItems.Prototype;
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
            this.MenuItems.Add(new SeparatorItem());
            this.MenuItems.Add(new ClickableItem("Cut", this.Cut));
            this.MenuItems.Add(new ClickableItem("Copy", this.Copy));
            this.MenuItems.Add(new ClickableItem("Paste", this.Paste, this.CanPaste));
            this.MenuItems.Add(new ClickableItem("Delete", this.Delete));
            this.MenuItems.Add(new ClickableItem("Rename", this.Rename));
        }

        private void AddNewSubGroupVM()
        {
            this.AddChild(new SubGroupVM(new SubGroup("New SubGroup " + _newCount)));
            _newCount++;
        }

        private void Cut()
        {

        }

        private void Copy()
        {

        }

        private void Paste()
        {

        }

        private bool CanPaste()
        {
            return false;
        }

        private void Delete()
        {

        }

        private void Rename()
        {

        }
    }
}
