using FactorioModBuilder.Models.ProjectItems.Prototype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype.Filters
{
    public class TechnologyFilterVM : FilterBaseVM<TechnologyVM>
    {
        private int _newFilter = 1;

        public TechnologyFilterVM(string name)
            : base(name, "Tech")
        {
        }

        protected override FilterBaseVM<TechnologyVM> GetNewFilter()
        {
            var res = new TechnologyFilterVM("New Sub Filter " + _newFilter);
            _newFilter++;
            return res;
        }

        protected override TechnologyVM GetNewChild()
        {
            return new TechnologyVM(new Technology("New Tech"));
        }
    }
}
