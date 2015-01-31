using FactorioModBuilder.Models.ProjectItems.Prototype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype.Filters
{
    public class EquipmentFilterVM : FilterBaseVM<EquipmentVM>
    {
        private int _newFilter = 1;
        private int _newEquip = 1;

        public EquipmentFilterVM(string name)
            : base(name, "Equipment")
        {
        }

        protected override FilterBaseVM<EquipmentVM> GetNewFilter()
        {
            return new EquipmentFilterVM("New Sub Filter " + _newFilter++);
        }

        protected override EquipmentVM GetNewChild()
        {
            return new EquipmentVM(new Equipment("New Equipment " + _newEquip++));
        }
    }
}
