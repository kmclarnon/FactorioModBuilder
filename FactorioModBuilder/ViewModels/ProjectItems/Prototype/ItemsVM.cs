using FactorioModBuilder.Build.Data;
using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using FactorioModBuilder.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfUtils;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class ItemsVM : ProjectItem<Items, ItemsVM>
    {
        public ObservableCollection<ItemVM> ItemList { get; private set; }

        public override IEnumerable<DataUnit> CompilerData
        {
            get { return this.ItemList.SelectMany(o => o.CompilerData); }
        }

        public ObservableCollection<SubGroupVM> PossibleSubgroups
        {
            get
            {
                PrototypesVM res;
                if(!this.TryFindElementUp(out res))
                    throw new Exception("Failed to find parent to supply PossibleSubgroups");
                return res.ItemSubgroups;
            }
        }

        public ObservableCollection<EntityVM> PossiblePlaceResults
        {
            get
            {
                PrototypesVM res;
                if (!this.TryFindElementUp(out res))
                    throw new Exception("Failed to find parent to supply PossiblePlaceResults");
                return res.Entities;
            }
        }

        public ICommand AddItemCmd { get { return this.GetCommand(this.AddItem); } }
        public ICommand RemoveItemCmd { get { return this.GetCommand(this.RemoveItem, this.CanRemoveItem); } }

        private int _newCount = 1;

        public ItemsVM(Items items)
            : this(null, items)
        {
        }

        public ItemsVM(TreeItemVMBase parent, Items items)
            : base(parent, items)
        {
            this.ItemList = new ObservableCollection<ItemVM>();
        }

        /// <summary>
        /// Adds a new item to the ItemList collection
        /// </summary>
        private void AddItem()
        {
            this.ItemList.Add(new ItemVM(this, 
                new Item("new-item-" + _newCount)));
            _newCount++;
        }

        /// <summary>
        /// Determines if any items can be removed from the ItemList collection
        /// </summary>
        /// <returns>True if any items are selected, otherwise false</returns>
        private bool CanRemoveItem()
        {
            return this.ItemList.Any(o => o.IsSelected);
        }

        /// <summary>
        /// Removes all selected items from the ItemList collection
        /// </summary>
        private void RemoveItem()
        {
            this.ItemList.RemoveWhere(o => o.IsSelected);
        }
    }
}
