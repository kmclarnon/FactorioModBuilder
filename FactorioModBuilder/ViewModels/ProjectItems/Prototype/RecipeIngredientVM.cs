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
            set { this.SetProperty(value, (() => this.Name = (value == null) ? String.Empty : value.Name)); }
        }

        public int Quantity
        {
            get { return _internal.Quantity; }
            set { this.SetProperty(_internal, value); }
        }

        public RecipeIngredientVM(RecipeIngredient item)
            : base(item)
        {
        }

        public RecipeIngredientVM(TreeItemVMBase parent, RecipeIngredient item)
            : base(parent, item)
        {
        }
    }
}
