using FactorioModBuilder.Models.ProjectItems.Prototype;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class PrototypesVM : ProjectItemVM
    {
        public ObservableCollection<SubGroupVM> PossibleSubgroups
        {
            get
            {
                SubGroupsVM res;
                if (!this.TryFindElementDown<SubGroupsVM>(out res))
                    throw new Exception("Could not find subgroups child element");
                return res.ItemList;
            }
        }

        public PrototypesVM(ProjectItemVM parent, Prototypes types)
            : base(parent, types)
        {
            this.Children.Add(new GroupsVM(this, new Groups()));
            this.Children.Add(new SubGroupsVM(this, new SubGroups()));
            this.Children.Add(new ItemsVM(this, new Items()));
            this.Children.Add(new EntityVM(this, new Entity()));
        }
    }
}
