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
    /// <summary>
    /// The base class for project items.  All project items implement ICompilerSource so thate
    /// they can provide information to the compiler in a consisten manner
    /// </summary>
    /// <typeparam name="TModel">The type of the model to wrap</typeparam>
    /// <typeparam name="TViewModel">The type of the this view model (CRTP)</typeparam>
    public abstract class ProjectItem<TModel, TViewModel> : TreeItemVM<TModel, TViewModel>, ICompilerSource 
        where TModel : TreeItem<TModel>
        where TViewModel : TreeItemVM<TModel, TViewModel>
    {
        /// <summary>
        /// Provides a list of DataUnits generated from the data contained in the associated view models
        /// </summary>
        public abstract IEnumerable<DataUnit> CompilerData { get; }

        /// <summary>
        /// The basic constructor to wrap the given model in its associated view model
        /// </summary>
        /// <param name="item"></param>
        public ProjectItem(TModel item) 
            : base(item)
        {
        }

        /// <summary>
        /// The constructor to wrap the given model in the appropriate view model
        /// and associate it with the given parent
        /// </summary>
        /// <param name="parent">The parent view model</param>
        /// <param name="item">The model to wrap</param>
        public ProjectItem(TreeItemVMBase parent, TModel item)
            : base(parent, item)
        {
        }

        /// <summary>
        /// The constructor to wrap the given model in the appropriate view model
        /// and give it the provided children
        /// </summary>
        /// <param name="item">The model to wrap</param>
        /// <param name="children">The view model children of this model</param>
        public ProjectItem(TModel item, IEnumerable<TreeItemVMBase> children)
            : base(item, children)
        {
        }

        /// <summary>
        /// The constructor to wrap the given model in the appropriate view model,
        /// give it the provided children and associate it with the given parent
        /// </summary>
        /// <param name="parent">The parent view model</param>
        /// <param name="item">The model to wrap</param>
        /// <param name="childre">The view model children of this model</param>
        public ProjectItem(TreeItemVMBase parent, TModel item, IEnumerable<TreeItemVMBase> childre)
            : base(parent, item, childre)
        {
        }
    }
}
