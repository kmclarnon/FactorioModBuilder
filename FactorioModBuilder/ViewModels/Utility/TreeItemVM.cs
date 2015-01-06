using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfUtils;
using FactorioModBuilder.Models;
using FactorioModBuilder.Models.ProjectItems;
using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.ProjectItems.Prototype;
using FactorioModBuilder.Models.Utility;

namespace FactorioModBuilder.ViewModels.Utility
{
    public abstract class TreeItemVM<T> : TreeItemVMBase
        where T : TreeItem<T>
    {
        protected T _internal { get { return (T)_item; } }

        public TreeItemVM(TreeItemVMBase parent, T item)
            : base(parent, item)
        {
        }
    }
}
