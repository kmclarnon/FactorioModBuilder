using FactorioModBuilder.Build.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Data
{
    public class RecipeData : DataUnit
    {
        public string Name { get; private set; }
        public bool Enabled { get; private set; }
        public List<Tuple<string, int>> Ingredients { get; private set; }
        public int EnergyReq { get; private set; }
        public string Result { get; private set; }

        public RecipeData(string name, bool enabled, 
            IEnumerable<Tuple<string, int>> ingredients, int energyReq, string result) 
            : base(ExtensionType.PrototypeRecipes)
        {
            this.Name = name;
            this.Enabled = enabled;
            this.Ingredients = ingredients.ToList();
            this.EnergyReq = energyReq;
            this.Result = result;
        }
    }
}
