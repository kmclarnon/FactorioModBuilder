using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfUtils.Extensions;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class TechnologyIngredientVM : ProjectItem<TechnologyIngredient, TechnologyIngredientVM>
    {
        public override IEnumerable<Build.Data.DataUnit> CompilerData
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// The quantity of this ingredient that is required
        /// </summary>
        public int Quantity
        {
            get { return _internal.Quantity; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The item that is represented by this ingredient
        /// </summary>
        public ItemVM Ingredient
        {
            get { return this.GetProperty<ItemVM>(); }
            set 
            { 
                this.SetProperty(value,
                    this.HandleItemBinding,
                    (() => this.Name = (value == null) ? String.Empty : value.Name)); 
            }
        }

        public TechnologyIngredientVM(TechnologyIngredient item)
            : this(null, item)
        {
        }

        public TechnologyIngredientVM(TreeItemVMBase parent, TechnologyIngredient item)
            : base(parent, item)
        {
        }

        /// <summary>
        /// Handles hooking and unhooking the ItemVM's property changed notifications
        /// in order to catch renaming
        /// </summary>
        /// <param name="val"></param>
        private void HandleItemBinding(ItemVM val)
        {
            if (this.Ingredient != null)
                this.Ingredient.PropertyChanged -= this.IngredientPropertyChanged;
            if (val != null)
                val.PropertyChanged += this.IngredientPropertyChanged;
        }

        /// <summary>
        /// Rereads the name property of the ItemVM whenever there is a property change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IngredientPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.Ingredient == null)
                this.Name = String.Empty;
            else
                this.Name = this.Ingredient.Name;
        }
    }
}
