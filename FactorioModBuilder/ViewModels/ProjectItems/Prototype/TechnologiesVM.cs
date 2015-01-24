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
using System.Collections.Specialized;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    /// <summary>
    /// A view model for a the technology screen
    /// </summary>
    public class TechnologiesVM : TreeItemVM<Technologies, TechnologiesVM>
    {
        /// <summary>
        /// The list of created technologies
        /// </summary>
        public ObservableCollection<TechnologyVM> ItemList { get; private set; }

        /// <summary>
        /// Command binding to add a new technology
        /// </summary>
        public ICommand AddTechCmd { get { return this.GetCommand(this.AddTech); } }

        /// <summary>
        /// Command binding to remove selected technologies
        /// </summary>
        public ICommand RemoveTechCmd { get { return this.GetCommand(this.RemoveTech, this.CanRemoveTech); } }

        /// <summary>
        /// Recipes available for selection as unlock targets
        /// </summary>
        public ObservableCollection<RecipeVM> AvailableRecipes
        {
            get
            {
                PrototypesVM res;
                if (!this.TryFindElementUp(out res))
                    throw new Exception("Could not find prototypes parent element");
                return res.Recipes;
            }
        }

        private int _newCount = 1;

        public TechnologiesVM(Technologies tech)
            : this(null, tech)
        {
        }

        public TechnologiesVM(TreeItemVMBase parent, Technologies tech)
            : base(parent, tech)
        {
            this.ItemList = new ObservableCollection<TechnologyVM>();
        }

        protected override void Initialize()
        {
            this.AvailableRecipes.CollectionChanged += AvailableRecipesChanged;
            this.ItemList.CollectionChanged += ItemListChanged;
        }

        /// <summary>
        /// Handles the removal of technologies referenced as prerequisites in other technologies
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ItemListChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach(TechnologyVM oldt in e.OldItems)
                {
                    foreach (var t in this.ItemList)
                        t.ForceRemoveTechnology(t);
                }
            }
        }

        /// <summary>
        /// Handles removal of recipes that were referenced in effects of technologies
        /// </summary>
        void AvailableRecipesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach(RecipeVM r in e.OldItems)
                {
                    foreach (var t in this.ItemList)
                        t.ForceRemoveRecipe(r);
                }
            }
        }

        /// <summary>
        /// Adds a new technology to ItemList
        /// </summary>
        private void AddTech()
        {
            this.ItemList.Add(new TechnologyVM(
                new Technology("new-technology-" + _newCount)));
            _newCount++;
        }

        /// <summary>
        /// Returns whether any selected technologies can be removed
        /// </summary>
        /// <returns>Returns true if any technologies are selected</returns>
        private bool CanRemoveTech()
        {
            return this.ItemList.Any(o => o.IsSelected);
        }

        /// <summary>
        /// Removes the selected tech from ItemList
        /// </summary>
        private void RemoveTech()
        {
            this.ItemList.RemoveWhere(o => o.IsSelected);
        }
    }
}
