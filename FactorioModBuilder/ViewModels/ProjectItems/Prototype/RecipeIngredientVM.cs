using FactorioModBuilder.Build.Data;
using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class RecipeIngredientVM : ProjectItem<RecipeIngredient, RecipeIngredientVM>
    {
        public override IEnumerable<DataUnit> CompilerData
        {
            get { throw new NotImplementedException(); }
        }

        public TreeItemVMBase Ingredient
        {
            get { return this.GetProperty<TreeItemVMBase>(); }
            set { this.SetProperty(value, false, this.UpdateSubGroupBinding, (x => this.Name = x.Name)); }
        }

        public int Quantity
        {
            get { return this.GetProperty<int>(); }
            set { this.SetProperty(value); }
        }

        public RecipeIngredientVM(RecipeIngredient item)
            : base(item)
        {
        }

        public RecipeIngredientVM(TreeItemVMBase parent, RecipeIngredient item)
            : base(parent, item)
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
