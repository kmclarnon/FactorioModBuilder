using FactorioModBuilder.Build.Data;
using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
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

        public ICommand AddItemCmd { get { return this.GetCommand(this.AddItem, this.CanAddItem); } }
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

        private bool CanAddItem()
        {
            return true;
        }

        private void AddItem()
        {
            this.ItemList.Add(new ItemVM(this, 
                new Item("new-item-" + _newCount)));
            _newCount++;
        }

        private bool CanRemoveItem()
        {
            return this.ItemList.Where(o => o.IsSelected).Any();
        }

        private void RemoveItem()
        {
            var list = this.ItemList.Where(o => o.IsSelected).ToList();
            foreach (var i in list)
                this.ItemList.Remove(i);
        }
    }
}
