using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype.Filters
{
    public class EntityFilterVM : FilterBaseVM<EntityVM>
    {
        public EntityFilterVM(string name)
            : base(name)
        {
        }
    }
}
