using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype.Filters
{
    /// <summary>
    /// A Filter for Item Prototypes
    /// </summary>
    public class ItemFilterVM : FilterBaseVM<ItemVM>
    {
        /// <summary>
        /// A counter to keep track of new items created
        /// </summary>
        private int _newCount = 1;

        public ItemFilterVM(string name)
            : base(name)
        {
        }

        protected override FilterBaseVM<ItemVM> GetNewFilter()
        {
            return new ItemFilterVM("New Filter");
        }

        protected override ItemVM GetNewChild()
        {
            return new ItemVM(new Models.ProjectItems.Prototype.Item("New Item " + _newCount++));
        }
    }
}
