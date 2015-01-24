using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class TechnologyPrerequisiteVM 
        : ProjectItem<TechnologyPrerequisite, TechnologyPrerequisiteVM>
    {
        public override IEnumerable<Build.Data.DataUnit> CompilerData
        {
            get { throw new NotImplementedException(); }
        }

        public TechnologyPrerequisiteVM(TechnologyPrerequisite item)
            : this(null, item)
        {
        }

        public TechnologyPrerequisiteVM(TreeItemVMBase parent, 
            TechnologyPrerequisite item)
            : base(parent, item)
        {
        }
    }
}
