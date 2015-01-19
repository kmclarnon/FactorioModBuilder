using FactorioModBuilder.Build;
using FactorioModBuilder.Build.Data;
using FactorioModBuilder.Models.Base;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public abstract class ProjectItem<TItem, TViewModel> : TreeItemVM<TItem, TViewModel>, ICompilerSource 
        where TItem : TreeItem<TItem>
        where TViewModel : TreeItemVM<TItem, TViewModel>
    {
        public abstract IEnumerable<DataUnit> CompilerData { get; }

        public ProjectItem(TItem item) 
            : base(item)
        {
        }

        public ProjectItem(TreeItemVMBase parent, TItem item)
            : base(parent, item)
        {
        }

        public ProjectItem(TItem item, IEnumerable<TreeItemVMBase> children)
            : base(item, children)
        {
        }

        public ProjectItem(TreeItemVMBase parent, TItem item, IEnumerable<TreeItemVMBase> childre)
            : base(parent, item, childre)
        {
        }
    }
}
