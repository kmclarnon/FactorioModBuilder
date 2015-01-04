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

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public abstract class ProjectItemVM<T> : ProjectItemVMBase
        where T : ProjectItem<T>
    {
        protected T _internal { get { return (T)_item; } }

        public ProjectItemVM(ProjectItemVMBase parent, T item)
            : base(parent, item)
        {
        }
    }
}
