using FactorioModBuilder.Build;
using FactorioModBuilder.Build.Data;
using FactorioModBuilder.Build.Extensions;
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
    public class ModInfoDependencyVM : ProjectItem<ModInfoDependency, ModInfoDependencyVM>
    {
        public override IEnumerable<DataUnit> CompilerData
        {
            get
            {
                return new ModInfoDependencyData(this.Name, this.Version, this.Optional).ListWrap();
            }
        }

        public bool Optional
        {
            get { return this.GetProperty<bool>(); }
            set { this.SetProperty(value); }
        }

        public bool Enabled
        {
            get { return this.GetProperty<bool>(); }
            set { this.SetProperty(value); }
        }

        public string Version
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
        }

        public ModInfoDependencyVM(ModInfoDependency item)
            : base(item)
        {
        }

        public ModInfoDependencyVM(TreeItemVMBase parent, ModInfoDependency item)
            : base(parent, item)
        {
        }
    }
}
