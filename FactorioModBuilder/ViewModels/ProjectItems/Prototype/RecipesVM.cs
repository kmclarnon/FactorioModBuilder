using FactorioModBuilder.Build.Data;
using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using FactorioModBuilder.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class RecipesVM : ProjectItem<Recipes, RecipesVM>
    {
        public override IEnumerable<DataUnit> CompilerData
        {
            get
            {
                return this.ItemList.Select(o => 
                    new RecipeData(o.Name, o.Enabled, 
                        o.Ingredients.Select(e => new Tuple<string, int>(e.Name, e.Quantity)),
                        o.EnergyRequired,
                        o.Result,
                        o.ResultCount));
            }
        }

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

        public ICommand AddRecipeCmd { get { return this.GetCommand(this.AddRecipe); } }
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

        protected override void Initialize()
        {
            this.PossibleResults.CollectionChanged += PossibleResultsCollectionChanged;
            this.PossibleIngredients.CollectionChanged += PossibleIngredientsCollectionChanged;
        }

        void PossibleIngredientsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach(ItemVM item in e.OldItems)
                {
                    foreach(var r in this.ItemList)
                        r.ForceRemoveIngredient(item);
                }
            }
        }

        void PossibleResultsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach(ItemVM i in e.OldItems)
                {
                    foreach (var r in this.ItemList)
                        if (r.ResultItem == i)
                            r.ForceRemoveResultItem();
                }
            }
        }

        /// <summary>
        /// Adds a new recipe to the ItemList collection
        /// </summary>
        private void AddRecipe()
        {
            this.ItemList.Add(new RecipeVM(
                new Recipe("new-recipe-" + _newCount)));
            _newCount++;
        }

        /// <summary>
        /// Determines whether any recipes can be removed from the ItemsList collection
        /// </summary>
        /// <returns>True if any recipes are selected, otherwise false</returns>
        private bool CanRemoveRecipe()
        {
            return this.ItemList.Any(o => o.IsSelected);
        }

        /// <summary>
        /// Removes all selected recipes from the ItemList collection
        /// </summary>
        private void RemoveRecipe()
        {
            this.ItemList.RemoveWhere(o => o.IsSelected);
        }
    }
}
