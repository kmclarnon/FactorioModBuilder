using FactorioModBuilder.Models.ProjectItems;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class PrototypesVM : ProjectItemVM
    {
        public ObservableCollection<SubGroupVM> PossibleSubgroups
        {
            get
            {
                ProjectItemVM res;
                if (!this.TryFindElementWithPropertyDown(typeof(ObservableCollection<SubGroupVM>),
                    "SubgroupList", out res))
                {
                    throw new Exception("Could not find appropriate child element to populate Possible Subgroups");
                }

                return (ObservableCollection<SubGroupVM>)res.GetType()
                    .GetProperty("SubgroupList").GetValue(res);
            }
        }

        public PrototypesVM(ProjectItemVM parent, Prototypes types)
            : base(parent, types)
        {
            this.Children.Add(new GroupsVM(this, new Groups()));
            this.Children.Add(new SubGroupsVM(this, new SubGroups()));
            this.Children.Add(new GameItemsVM(this, new GameItems()));
        }
    }
}
