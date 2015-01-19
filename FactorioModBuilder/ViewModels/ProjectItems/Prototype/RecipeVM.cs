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
            set { this.SetProperty(value, (() => this.Result = (value == null) ? String.Empty : value.Name)); }
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

        public RecipeVM(Recipe rec)
            : this(null, rec)
        {
        }

        public RecipeVM(TreeItemVMBase parent, Recipe rec)
            : base(parent, rec)
        {
            this.Ingredients = new ObservableCollection<RecipeIngredientVM>();
        }
    }
}
