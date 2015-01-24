using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public TechnologyIngredientVM(TechnologyIngredient item)
            : this(null, item)
        {
        }

        public TechnologyIngredientVM(TreeItemVMBase parent, TechnologyIngredient item)
            : base(parent, item)
        {
        }
    }
}
