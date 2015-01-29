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
using WpfUtils.Extensions;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class RecipeVM : ProjectItem<Recipe, RecipeVM>
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

        public ICommand AddIngredientCmd { get { return this.GetCommand(this.AddIngredient); } }
        public ICommand RemoveIngredientCmd { get { return this.GetCommand(this.RemoveIngredient, this.CanRemoveIngredient); } }

        private int _newCount = 1;

        public RecipeVM(Recipe rec)
            : this(null, rec)
        {
        }

        public RecipeVM(TreeItemVMBase parent, Recipe rec)
            : base(parent, rec, DoubleClickBehavior.OpenContent)
        {
            this.Ingredients = new ObservableCollection<RecipeIngredientVM>();
        }

        /// <summary>
        /// Adds a new ingredient to the Ingredients collection
        /// </summary>
        private void AddIngredient()
        {
            this.Ingredients.Add(new RecipeIngredientVM(
                new RecipeIngredient("New Ingredient " + _newCount, 1)));
            _newCount++;
        }

        /// <summary>
        /// Determines whether any ingredients can be removed
        /// </summary>
        /// <returns>True if any ingredients are selected, false otherwise</returns>
        private bool CanRemoveIngredient()
        {
            return this.Ingredients.Any(o => o.IsSelected);
        }

        /// <summary>
        /// Removes all selected ingredients from the Ingredients collection
        /// </summary>
        private void RemoveIngredient()
        {
            this.Ingredients.RemoveWhere(o => o.IsSelected);
        }

        /// <summary>
        /// Handles hooking and unhooking the ItemVM's property changed notification to catch renaming
        /// </summary>
        /// <param name="val">The potentially new ItemVM value</param>
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
            this.SetProperty<ItemVM>(null, null, null, true, 
                PropertyMethods.GetName(() => this.ResultItem));
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
