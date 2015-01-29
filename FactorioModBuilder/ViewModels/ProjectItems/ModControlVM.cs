using FactorioModBuilder.Build;
using FactorioModBuilder.Build.Data;
using FactorioModBuilder.Models.ProjectItems;
using FactorioModBuilder.ViewModels.Base;
using FactorioModBuilder.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class ModControlVM : ProjectItem<ModControl, ModControlVM>
    {
        public override IEnumerable<DataUnit> CompilerData
        {
            get
            {
                return new ModControlData().ListWrap();
            }
        }

        public ModControlVM(ModControl control)
            : base(control, DoubleClickBehavior.OpenContent)
        {
        }

        public ModControlVM(TreeItemVMBase parent, ModControl control)
            : base(parent, control, DoubleClickBehavior.OpenContent)
        {
        }
    }
}
