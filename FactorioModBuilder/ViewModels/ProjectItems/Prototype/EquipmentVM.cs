using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class EquipmentVM : TreeItemVM<Equipment>
    {
        public EquipmentVM(Equipment equip)
            : base(equip)
        {
        }

        public EquipmentVM(TreeItemVMBase parent, Equipment equip)
            : base(parent, equip)
        {
        }
    }
}
