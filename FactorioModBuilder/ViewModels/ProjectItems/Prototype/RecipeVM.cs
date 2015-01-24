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
            set 
            { 
                this.SetProperty(value, 
                    this.UpdateItemBinding, 
                    (() => this.Result = (value == null) ? String.Empty : value.Name)); 
            }
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

        public int EnergyRequired
        {
            get { return _internal.EnergyRequired; }
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

        /// <summary>
        /// Handles hooking and unhooking the ItemVM's property changed notification to catch renaming
        /// </summary>
        private void UpdateItemBinding(ItemVM val)
        {
            if (this.ResultItem != null)
                this.ResultItem.PropertyChanged -= ItemPropertyChanged;
            if (val != null)
                val.PropertyChanged += ItemPropertyChanged;
        }

        /// <summary>
        /// Re-reads the name property from the item whenever there is a property change
        /// </summary>
        void ItemPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (this.ResultItem == null)
                this.Result = String.Empty;
            else
                this.Result = this.ResultItem.Name;
        }

        /// <summary>
        /// Used to force the ResultItem value to null when the specified item
        /// was deleted by the user
        /// </summary>
        public void ForceRemoveResultItem()
        {
            this.SetProperty<ItemVM>(null, null, null, true, "ResultItem");
            this.Result = String.Empty;
        }

        /// <summary>
        /// Used to remove ingredients appropriate when the item specified is
        /// deleted by the user
        /// </summary>
        /// <param name="ingredient"></param>
        public void ForceRemoveIngredient(TreeItemVMBase ingredient)
        {
            var tmpList = this.Ingredients.ToList();
            foreach(var i in tmpList)
            {
                if (i.Ingredient == ingredient)
                    this.Ingredients.Remove(i);
            }
        }
    }
}
