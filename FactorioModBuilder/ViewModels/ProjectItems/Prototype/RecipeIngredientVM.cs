using FactorioModBuilder.Build.Data;
using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    /// <summary>
    /// View model for recipe ingredients (Items/Fluids)
    /// </summary>
    public class RecipeIngredientVM : ProjectItem<RecipeIngredient, RecipeIngredientVM>
    {
        /// <summary>
        /// Data to provide to the compiler at build time
        /// </summary>
        public override IEnumerable<DataUnit> CompilerData
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// The currently selected ingredient
        /// </summary>
        public TreeItemVMBase Ingredient
        {
            get { return this.GetProperty<TreeItemVMBase>(); }
            set { this.SetProperty(value, false, this.UpdateSubGroupBinding, (x => this.Name = value.Name)); }
        }

        /// <summary>
        /// The quantity of the ingredient that is required for the parent recipe
        /// </summary>
        public int Quantity
        {
            get { return this.GetProperty<int>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The possible items that could be selected for this ingredient
        /// </summary>
        public ObservableCollection<ItemVM> Ingredients
        {
            get
            {
                PrototypesVM pvm;
                if (!this.TryFindElementUp(out pvm))
                    throw new Exception("Could not find prototypes parent");
                return pvm.Items;
            }
        }

        public RecipeIngredientVM(RecipeIngredient item)
            : base(item, DoubleClickBehavior.OpenContent)
        {
        }

        public RecipeIngredientVM(TreeItemVMBase parent, RecipeIngredient item)
            : base(parent, item, DoubleClickBehavior.OpenContent)
        {
        }

        /// <summary>
        /// Handles hooking and unhooking the ingredient's property changed notification to catch renaming
        /// </summary>
        private void UpdateSubGroupBinding(TreeItemVMBase val)
        {
            if (this.Ingredient != null)
                this.Ingredient.PropertyChanged -= SubGroupPropertyChanged;
            if (val != null)
                val.PropertyChanged += SubGroupPropertyChanged;
        }

        /// <summary>
        /// Re-reads the name property from the ingredient whenever there is a property change
        /// </summary>
        void SubGroupPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (this.Ingredient == null)
                this.Name = String.Empty;
            else
                this.Name = this.Ingredient.Name;
        }
    }
}
