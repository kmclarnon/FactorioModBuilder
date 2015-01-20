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
        /// <summary>
        /// The Entities contained in this view model
        /// </summary>
        public ObservableCollection<EntityVM> ItemList { get; private set; }

        /// <summary>
        /// The basic constructor to wrap an Entities model
        /// </summary>
        /// <param name="en">The entities model to wrap</param>
        public EntitiesVM(Entities en)
            : this(null, en)
        {
        }

        /// <summary>
        /// THe basic constructo to wrap an Entities model and associate it with the parent TreeView view model
        /// </summary>
        /// <param name="parent">The parent of this view model</param>
        /// <param name="en">The entities model to wrap</param>
        public EntitiesVM(TreeItemVMBase parent, Entities en)
            : base(parent, en)
        {
            this.ItemList = new ObservableCollection<EntityVM>();
        }
    }
}
