using FactorioModBuilder.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems.Prototype
{
    public class TechnologyIngredient : TreeItem<TechnologyIngredient>
    {
        public int Quantity { get; set; }

        public TechnologyIngredient(string name, int quantity)
            : base(name)
        {
            this.Quantity = quantity;
        }
    }
}
