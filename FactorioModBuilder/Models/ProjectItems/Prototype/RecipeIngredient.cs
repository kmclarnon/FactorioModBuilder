using FactorioModBuilder.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems.Prototype
{
    public class RecipeIngredient : TreeItem<RecipeIngredient>
    {
        public int Quantity { get; set; }

        public RecipeIngredient(string name, int quantity)
            : base(name)
        {
            this.Quantity = quantity;
        }
    }
}
