using FactorioModBuilder.Build;
using FactorioModBuilder.Build.Extensions;
using FactorioModBuilder.Models.ProjectItems;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class ModInfoDependencyVM : ProjectItem<ModInfoDependency, ModInfoDependencyVM>
    {
        public override CompileUnit CompilerData
        {
            get
            {
                return new CompileUnit()
                {
                    { "Optional", new CompileUnit(this.Optional) },
                    { "Version", new CompileUnit(this.Version) }
                };
            }
        }

        public bool Optional
        {
            get { return _internal.Optional; }
            set { this.SetProperty(_internal, value); }
        }

        public bool Enabled
        {
            get { return _internal.Enabled; }
            set { this.SetProperty(_internal, value); }
        }

        public string Version
        {
            get { return _internal.Version; }
            set { this.SetProperty(_internal, value); }
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
