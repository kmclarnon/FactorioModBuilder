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
    /// <summary>
    /// View model to for a recipe prototype
    /// </summary>
    public class RecipeVM : ProjectItem<Recipe, RecipeVM>
    {
        /// <summary>
        /// Whether or not this recipe is enabled at the start of the game
        /// </summary>
        public bool Enabled
        {
            get { return this.GetProperty<bool>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The item that is created by this recipe
        /// </summary>
        public ItemVM ResultItem
        {
            get { return this.GetProperty<ItemVM>(); }
            set {  this.SetProperty(value, false, this.UpdateItemBinding, (x => this.Result = (value == null) ? String.Empty : value.Name)); }
        }

        /// <summary>
        /// The name of the item that is created by this recipe
        /// </summary>
        public string Result
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The number of result items that are produced by this recipe
        /// </summary>
        public int ResultCount
        {
            get { return this.GetProperty<int>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The energy required to use this recipe
        /// </summary>
        public int EnergyRequired
        {
            get { return this.GetProperty<int>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// Command binding for adding an ingredient to this recipe
        /// </summary>
        public ICommand AddIngredientCmd { get { return this.GetCommand(this.AddIngredient); } }

        /// <summary>
        /// Command binding for removing selected ingredients from this recipe
        /// </summary>
        public ICommand RemoveIngredientCmd { get { return this.GetCommand(this.RemoveIngredient, this.CanRemoveIngredient); } }

        private int _newCount = 1;

        public RecipeVM(Recipe rec)
            : this(null, rec)
        {
        }

        public RecipeVM(TreeItemVMBase parent, Recipe rec)
            : base(parent, rec, DoubleClickBehavior.OpenContent)
        {
        }

        /// <summary>
        /// Adds a new ingredient to the Ingredients collection
        /// </summary>
        private void AddIngredient()
        {
            this.Children.Add(new RecipeIngredientVM(
                new RecipeIngredient("New Ingredient " + _newCount, 1)));
            _newCount++;
        }

        /// <summary>
        /// Determines whether any ingredients can be removed
        /// </summary>
        /// <returns>True if any ingredients are selected, false otherwise</returns>
        private bool CanRemoveIngredient()
        {
            return this.Children.Any(o => o.IsSelected);
        }

        /// <summary>
        /// Removes all selected ingredients from the Ingredients collection
        /// </summary>
        private void RemoveIngredient()
        {
            this.Children.RemoveWhere(o => o.IsSelected);
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
            this.SetProperty(null, (() => this.ResultItem));
            this.Result = String.Empty;
        }

        /// <summary>
        /// Used to remove ingredients appropriate when the item specified is
        /// deleted by the user
        /// </summary>
        /// <param name="ingredient"></param>
        public void ForceRemoveIngredient(TreeItemVMBase ingredient)
        {
            var tmpList = this.Children.ToList();
            foreach(var i in tmpList)
            {
                if (i is RecipeIngredientVM && ((RecipeIngredientVM)i).Ingredient == ingredient)
                    this.Children.Remove(i);
            }
        }
    }
}
