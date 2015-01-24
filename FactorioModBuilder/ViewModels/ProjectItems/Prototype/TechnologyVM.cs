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
    /// The view model to represent a technology
    /// </summary>
    public class TechnologyVM : TreeItemVM<Technology, TechnologyVM>
    {
        /// <summary>
        /// The file path to this technology's icon image
        /// </summary>
        public string IconPath
        {
            get { return _internal.IconPath; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The number of research units that must be performed to research this technology
        /// </summary>
        public int UnitCount
        {
            get { return _internal.UnitCount; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The time, in seconds, that each unit takes to process
        /// </summary>
        public int UnitTime
        {
            get { return _internal.UnitTime; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The string used to determine display order for this technology
        /// </summary>
        public string Order
        {
            get { return _internal.Order; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The upgrade property of the recipe. Uncertain what this actually does
        /// </summary>
        public bool Upgrade
        {
            get { return _internal.Upgrade; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The list of ingredients required to process a single unit
        /// </summary>
        public ObservableCollection<TechnologyIngredientVM> UnitIngredients { get; private set; }

        /// <summary>
        /// The effects that are applied when this research is completed
        /// </summary>
        public ObservableCollection<TechnologyEffectVM> Effects { get; private set; }

        public TechnologyVM(Technology tech)
            : this(null, tech)
        {
        }

        public TechnologyVM(TreeItemVMBase parent, Technology tech)
            : base(parent, tech)
        {
            this.UnitIngredients = new ObservableCollection<TechnologyIngredientVM>();
            this.Effects = new ObservableCollection<TechnologyEffectVM>();
        }
    }
}
