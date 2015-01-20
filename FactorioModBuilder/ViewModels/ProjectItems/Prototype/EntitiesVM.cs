using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class EntitiesVM : TreeItemVM<Entities, EntitiesVM>
    {
        public ObservableCollection<EntityVM> ItemList { get; private set; }

        public EntitiesVM(Entities en)
            : this(null, en)
        {
        }

        public EntitiesVM(TreeItemVMBase parent, Entities en)
            : base(parent, en)
        {
            this.ItemList = new ObservableCollection<EntityVM>();
        }
    }
}
