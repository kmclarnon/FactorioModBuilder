using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype.Filters
{
    /// <summary>
    /// A filter for Fluid prototypes
    /// </summary>
    public class FluidFilterVM : FilterBaseVM<FluidVM>
    {
        /// <summary>
        /// A counter to keep track of the new fluids created
        /// </summary>
        private int _newCount = 1;

        public FluidFilterVM(string name)
            : base(name, "Fluid")
        {
        }

        protected override FilterBaseVM<FluidVM> GetNewFilter()
        {
            return new FluidFilterVM("New Filter");
        }

        protected override FluidVM GetNewChild()
        {
            return new FluidVM(new Models.ProjectItems.Prototype.Fluid("New Fluid " + _newCount++));
        }
    }
}
