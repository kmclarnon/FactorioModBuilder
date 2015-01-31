using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype.Filters
{
    /// <summary>
    /// A Filter for SubGroup prototypes
    /// </summary>
    public class SubGroupFilterVM : FilterBaseVM<SubGroupVM>
    {
        /// <summary>
        /// Counter used to keep track of new children
        /// </summary>
        private int _newCount = 1;

        public SubGroupFilterVM(string name)
            : base(name, "SubGroup")
        {
        }

        protected override FilterBaseVM<SubGroupVM> GetNewFilter()
        {
            return new SubGroupFilterVM("New Filter");
        }

        protected override SubGroupVM GetNewChild()
        {
            return new SubGroupVM(new SubGroup("New SubGroup " + _newCount++));
        }
    }
}
