using FactorioModBuilder.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems.Prototype
{
    public class Recipe : TreeItem<Recipe>
    {
        public Recipe(string name) : base(name)
        {

        }
    }
}
