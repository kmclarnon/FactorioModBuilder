using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype.Filters
{
    public class EntityFilterVM : FilterBaseVM<EntityVM>
    {
        private int _newFilter = 1;

        public EntityFilterVM(string name)
            : base(name, "Entity")
        {
        }

        protected override FilterBaseVM<EntityVM> GetNewFilter()
        {
            var res = new EntityFilterVM("New Sub Filter " + _newFilter);
            _newFilter++;
            return res;
        }

        protected override EntityVM GetNewChild()
        {
            return new EntityVM(new Models.ProjectItems.Prototype.Entity());
        }
    }
}
