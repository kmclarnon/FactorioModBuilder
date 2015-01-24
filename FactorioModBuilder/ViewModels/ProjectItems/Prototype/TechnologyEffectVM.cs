using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class TechnologyEffectVM : ProjectItem<TechnologyEffect, TechnologyEffectVM>
    {
        public override IEnumerable<Build.Data.DataUnit> CompilerData
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// The type of effect that this is
        /// </summary>
        public TechEffectType Type
        {
            get { return _internal.Type; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The name of the recipe to unlock if this is a UnlockRecipe type effect
        /// </summary>
        public string Recipe
        {
            get { return _internal.Recipe; }
            set { this.SetProperty(_internal, value); }
        }

        public TechnologyEffectVM(TechnologyEffect item)
            : base(null, item)
        {
        }

        public TechnologyEffectVM(TreeItemVMBase parent, TechnologyEffect item)
            : base(parent, item)
        {
        }
    }
}
