using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Utility;
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
    public class ItemsVM : TreeItemVM<Items>
    {
        public ObservableCollection<ItemVM> ItemList { get; private set; }

        private ICommand _addItem;
        public ICommand AddItemCmd
        {
            get
            {
                if (_addItem == null)
                    _addItem = new RelayCommand(
                        (x => this.AddItem()), (x => this.CanAddItem()));
                return _addItem;
            }
        }

        private ICommand _removeItem;
        public ICommand RemoveItemCmd
        {
            get
            {
                if (_removeItem == null)
                    _removeItem = new RelayCommand(
                        (x => this.RemoveItem()), (x => this.CanRemoveItem()));
                return _removeItem;
            }
        }

        private int _newCount = 1;

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
                new Item("New Item " + _newCount)));
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
