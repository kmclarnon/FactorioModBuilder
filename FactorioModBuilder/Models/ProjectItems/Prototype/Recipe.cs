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
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public int EnergyRequired { get; set; }
        public string Result { get; set; }
        public int ResultCount { get; set; }

        public Recipe(string name) : base(name)
        {

        }
    }
}
