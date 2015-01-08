using FactorioModBuilder.Build;
using FactorioModBuilder.Models.Base;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public abstract class ProjectItem<T> : TreeItemVM<T>, ICompilerSource where T : TreeItem<T>
    {
        public abstract CompileUnit CompilerData { get; }
        public abstract string CompilerKey { get; }
        public IEnumerable<ICompilerSource> SubUnits
        {
            get { return this.Children.Cast<ICompilerSource>(); }
        }

        public ProjectItem(T item) 
            : base(item)
        {
        }

        public ProjectItem(TreeItemVMBase parent, T item)
            : base(parent, item)
        {
        }

        public ProjectItem(T item, IEnumerable<TreeItemVMBase> children)
            : base(item, children)
        {
        }

        public ProjectItem(TreeItemVMBase parent, T item, IEnumerable<TreeItemVMBase> childre)
            : base(parent, item, childre)
        {
        }
    }
}
