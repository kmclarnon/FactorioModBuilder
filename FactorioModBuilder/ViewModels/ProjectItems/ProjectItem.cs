using FactorioModBuilder.Build;
using FactorioModBuilder.Build.Data;
using FactorioModBuilder.Models.Base;
using FactorioModBuilder.ViewModels.Base;
using FactorioModBuilder.ViewModels.Menu.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public abstract class ProjectItem<TModel, TViewModel> : TreeItemVM<TModel, TViewModel>, ICompilerSource, IDoubleClickBehavior, IMenuProvider 
        where TModel : TreeItem<TModel>
        where TViewModel : TreeItemVM<TModel, TViewModel>
    {
        /// <summary>
        /// Provides a list of DataUnits generated from the data contained in the associated view models
        /// </summary>
        public virtual IEnumerable<DataUnit> CompilerData
        {
            get { return this.Children.Where(o => o is ICompilerSource).Cast<ICompilerSource>().SelectMany(o => o.CompilerData); }
        }

        public ObservableCollection<IMenuItemProvider> MenuItems { get; private set; }

        /// <summary>
        /// Whether this project item should be opened when the user double clicks on it
        /// </summary>
        public DoubleClickBehavior DoubleClickBehavior { get; private set; }

        /// <summary>
        /// The basic constructor to wrap the given model in its associated view model
        /// </summary>
        /// <param name="item"></param>
        /// <param name="openOnDblClick">Whether or not this project item should be opened when the user double clicks on it</param>
        public ProjectItem(TModel item, DoubleClickBehavior dblClickBehavior = DoubleClickBehavior.Ignore) 
            : this(null, item, dblClickBehavior)
        {
        }

        /// <summary>
        /// The constructor to wrap the given model in the appropriate view model
        /// and associate it with the given parent
        /// </summary>
        /// <param name="parent">The parent view model</param>
        /// <param name="item">The model to wrap</param>
        /// <param name="openOnDblClick">Whether or not this project item should be opened when the user double clicks on it</param>
        public ProjectItem(TreeItemVMBase parent, TModel item, DoubleClickBehavior dblClickBehavior = DoubleClickBehavior.Ignore)
            : base(parent, item)
        {
            this.DoubleClickBehavior = dblClickBehavior;
            this.MenuItems = new ObservableCollection<IMenuItemProvider>();
        }

        /// <summary>
        /// The constructor to wrap the given model in the appropriate view model
        /// and give it the provided children
        /// </summary>
        /// <param name="item">The model to wrap</param>
        /// <param name="children">The view model children of this model</param>
        /// <param name="openOnDblClick">Whether or not this project item should be opened when the user double clicks on it</param>
        public ProjectItem(TModel item, IEnumerable<TreeItemVMBase> children, DoubleClickBehavior dblClickBehavior = DoubleClickBehavior.Ignore)
            : this(null, item, children, dblClickBehavior)
        {
        }

        /// <summary>
        /// The constructor to wrap the given model in the appropriate view model,
        /// give it the provided children and associate it with the given parent
        /// </summary>
        /// <param name="parent">The parent view model</param>
        /// <param name="item">The model to wrap</param>
        /// <param name="childre">The view model children of this model</param>
        /// <param name="openOnDblClick">Whether or not this project item should be opened when the user double clicks on it</param>
        public ProjectItem(TreeItemVMBase parent, TModel item, IEnumerable<TreeItemVMBase> children, 
            DoubleClickBehavior dblClickBehavior = DoubleClickBehavior.Ignore)
            : base(parent, item, children)
        {
            this.DoubleClickBehavior = dblClickBehavior;
            this.MenuItems = new ObservableCollection<IMenuItemProvider>();
        }
    }
}
