using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype.Filters
{
    public class ItemFilterVM : FilterBaseVM<ItemVM>
    {
        public ItemFilterVM(string name)
            : base(name)
        {
        }
    }
}
