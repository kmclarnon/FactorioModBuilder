using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class TechnologyEffectVM : ProjectItem<TechnologyEffect, TechnologyEffectVM>
    {
        public override IEnumerable<Build.Data.DataUnit> CompilerData
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// The type of effect that this is
        /// </summary>
        public TechEffectType Type
        {
            get { return this.GetProperty<TechEffectType>(); ; }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The name of the recipe to unlock if this is a UnlockRecipe type effect
        /// </summary>
        public string Recipe
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The Recipe that may be unlocked by this technology effect
        /// </summary>
        public RecipeVM RecipeItem
        {
            get { return this.GetProperty<RecipeVM>(); }
            set { this.SetProperty(value, false, this.HandleRecipeBinding, (x => this.Recipe = x.Name)); }
        }

        public TechnologyEffectVM(TechnologyEffect item)
            : base(item)
        {
        }

        public TechnologyEffectVM(TreeItemVMBase parent, TechnologyEffect item)
            : base(parent, item)
        {
        }

        /// <summary>
        /// Handles hooking and unhooking the RecipeVM's property changed notifications
        /// in order to catch renaming
        /// </summary>
        /// <param name="rec">The potentially new RecipeVM value</param>
        private void HandleRecipeBinding(RecipeVM rec)
        {
            if(this.RecipeItem != null)
                this.RecipeItem.PropertyChanged -= this.RecipeItemPropertyChanged;
            if (rec != null)
                rec.PropertyChanged += this.RecipeItemPropertyChanged;
        }

        /// <summary>
        /// Rereads the name property form the RecipeVM whenever there is a 
        /// property change
        /// </summary>
        void RecipeItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.RecipeItem == null)
                this.Recipe = String.Empty;
            else
                this.Recipe = this.RecipeItem.Name;
        }
    }
}
