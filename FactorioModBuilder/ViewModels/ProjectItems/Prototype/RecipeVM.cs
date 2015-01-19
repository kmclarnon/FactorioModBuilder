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
    public class RecipeVM : TreeItemVM<Recipe, RecipeVM>
    {
        public ObservableCollection<RecipeIngredientVM> Ingredients { get; private set; }

        public bool Enabled
        {
            get { return _internal.Enabled; }
            set { this.SetProperty(_internal, value); }
        }

        public ItemVM ResultItem
        {
            get { return this.GetProperty<ItemVM>(); }
            set { this.SetProperty(value, (() => this.Result = (value == null) ? String.Empty : value.Name)); }
        }

        public string Result
        {
            get { return _internal.Result; }
            set { this.SetProperty(_internal, value); }
        }

        public int ResultCount
        {
            get { return _internal.ResultCount; }
            set { this.SetProperty(_internal, value); }
        }

        public ICommand AddIngredientCmd { get { return this.GetCommand(this.AddIngredient, this.CanAddIngredient); } }
        public ICommand RemoveIngredientCmd { get { return this.GetCommand(this.RemoveIngredient, this.CanRemoveIngredient); } }

        private int _newCount = 1;

        public RecipeVM(Recipe rec)
            : this(null, rec)
        {
        }

        public RecipeVM(TreeItemVMBase parent, Recipe rec)
            : base(parent, rec)
        {
            this.Ingredients = new ObservableCollection<RecipeIngredientVM>();
        }

        private bool CanAddIngredient()
        {
            return true;
        }

        private void AddIngredient()
        {
            this.Ingredients.Add(new RecipeIngredientVM(
                new RecipeIngredient("New Ingredient " + _newCount, 1)));
            _newCount++;
        }

        private bool CanRemoveIngredient()
        {
            return this.Ingredients.Where(o => o.IsSelected).Any();
        }

        private void RemoveIngredient()
        {
            var res = this.Ingredients.Where(o => o.IsSelected).ToList();
            foreach (var r in res)
                this.Ingredients.Remove(r);
        }
    }
}
