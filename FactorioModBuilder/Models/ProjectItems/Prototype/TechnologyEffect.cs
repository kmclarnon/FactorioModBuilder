using FactorioModBuilder.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems.Prototype
{
    public enum TechEffectType
    {
        [DescriptionAttribute("Unlock Recipe")]
        UnlockRecipe
    };

    public class TechnologyEffect : TreeItem<TechnologyEffect>
    {
        public TechEffectType Type { get; set; }
        public string Recipe { get; set; }

        public TechnologyEffect(TechEffectType type, string recipeName)
            : base(recipeName)
        {
            this.Type = type;
            this.Recipe = recipeName;
        }
    }
}
