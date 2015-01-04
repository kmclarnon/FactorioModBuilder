using FactorioModBuilder.Models.ProjectItems.Prototype;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class PrototypesVM : ProjectItemVM<Prototypes>
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

        public PrototypesVM(ProjectItemVMBase parent, Prototypes types)
            : base(parent, types)
        {
            this.Children.Add(new GroupsVM(this, new Groups()));
            this.Children.Add(new SubGroupsVM(this, new SubGroups()));
            this.Children.Add(new EntitiesVM(this, new Entities()));
            this.Children.Add(new EquipsVM(this, new Equips()));
            this.Children.Add(new FluidsVM(this, new Fluids()));
            this.Children.Add(new ItemsVM(this, new Items()));
            this.Children.Add(new RecipesVM(this, new Recipes()));
            this.Children.Add(new TechnologiesVM(this, new Technologies()));
            this.Children.Add(new TilesVM(this, new Tiles()));
        }
    }
}
