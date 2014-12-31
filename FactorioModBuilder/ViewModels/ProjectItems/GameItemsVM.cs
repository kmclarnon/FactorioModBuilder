using FactorioModBuilder.Models.ProjectItems;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class GameItemsVM : ProjectItemVM
    {
        public ObservableCollection<GameItemVM> ItemList { get; private set; }

        private GameItems _internal { get { return (GameItems)_item; } }

        public GameItemsVM(ProjectItemVM parent, GameItems items)
            : base(parent, items)
        {
            this.ItemList = new ObservableCollection<GameItemVM>();
            foreach (var c in _internal.ItemList)
                this.ItemList.Add(new GameItemVM(this, c));
        }
    }
}
