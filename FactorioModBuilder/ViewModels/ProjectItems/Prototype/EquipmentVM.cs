using FactorioModBuilder.Models.ProjectItems.Prototype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class EquipmentVM : ProjectItemVM<Equipment>
    {
        public EquipmentVM(ProjectItemVMBase parent, Equipment equip)
            : base(parent, equip)
        {
        }
    }
}
