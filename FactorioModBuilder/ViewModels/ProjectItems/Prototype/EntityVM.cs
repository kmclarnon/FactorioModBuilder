using FactorioModBuilder.Models.ProjectItems.Prototype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class EntityVM : ProjectItemVM<Entity>
    {
        public EntityVM(ProjectItemVMBase parent, Entity entity)
            : base(parent, entity)
        {
        }
    }
}
