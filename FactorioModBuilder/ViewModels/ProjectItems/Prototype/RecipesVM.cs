using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class RecipesVM : TreeItemVM<Recipes, RecipesVM>
    {
        public ObservableCollection<RecipeVM> ItemList { get; private set; }
        public ObservableCollection<ItemVM> PossibleResults
        {
            get
            {
                PrototypesVM res;
                if (!this.TryFindElementUp(out res))
                    throw new Exception("Could not find prototypes parent");
                return res.Items;
            }
        }

        public ObservableCollection<ItemVM> PossibleIngredients
        {
            get
            {
                PrototypesVM res;
                if (!this.TryFindElementUp(out res))
                    throw new Exception("Could not find prototypes parent");
                return res.Items;
            }
        }

        public ICommand AddRecipeCmd { get { return this.GetCommand(this.AddRecipe, this.CanAddRecipe); } }
        public ICommand RemoveRecipeCmd { get { return this.GetCommand(this.RemoveRecipe, this.CanRemoveRecipe); } }

        private int _newCount = 1;

        public RecipesVM(Recipes rec)
            : this(null, rec)
        {
        }

        public RecipesVM(TreeItemVMBase parent, Recipes rec)
            : base(parent, rec)
        {
            this.ItemList = new ObservableCollection<RecipeVM>();
        }

        private bool CanAddRecipe()
        {
            return true;
        }

        private void AddRecipe()
        {
            this.ItemList.Add(new RecipeVM(
                new Recipe("New Recipe " + _newCount)));
            _newCount++;
        }

        private bool CanRemoveRecipe()
        {
            return this.ItemList.Where(o => o.IsSelected).Any();
        }

        private void RemoveRecipe()
        {
            var res = this.ItemList.Where(o => o.IsSelected);
            foreach (var r in res)
                this.ItemList.Remove(r);
        }
    }
}
