using FactorioModBuilder.Build.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class RecipeExtension : ExtensionBase<RecipeData>
    {
        public RecipeExtension()
            : base(ExtensionType.PrototypeRecipes,
            ExtensionType.PrototypeItems)
        {
        }

        protected override bool BuildUnit(IEnumerable<RecipeData> units)
        {
            throw new NotImplementedException();
        }
    }
}
