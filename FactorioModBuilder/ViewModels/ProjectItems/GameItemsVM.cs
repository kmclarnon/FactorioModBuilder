using FactorioModBuilder.Models.ProjectItems;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfUtils;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class GameItemsVM : ProjectItemVM
    {
        public ObservableCollection<GameItemVM> ItemList { get; private set; }

        private GameItems _internal { get { return (GameItems)_item; } }

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

        public GameItemsVM(ProjectItemVM parent, GameItems items)
            : base(parent, items)
        {
            this.ItemList = new ObservableCollection<GameItemVM>();
        }

        private bool CanAddItem()
        {
            return true;
        }

        private void AddItem()
        {
            this.ItemList.Add(new GameItemVM(this, 
                new GameItem("New Item " + _newCount)));
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
