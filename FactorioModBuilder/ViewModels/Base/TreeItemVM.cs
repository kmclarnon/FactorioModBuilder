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
using FactorioModBuilder.Models.Base;

namespace FactorioModBuilder.ViewModels.Base
{
    /// <summary>
    /// The strongly typed TreeItem view model that provides a protected convenience property to access
    /// the wrapped model without casting.  Also provides a method of statically registering property validators and
    /// associated error messages to be exposed through the IDataErrorInfo interface
    /// </summary>
    /// <typeparam name="TModel">The type of the model to be wrapped by this view model</typeparam>
    /// <typeparam name="TViewModel">The type of the derived view model class (CRTP)</typeparam>
    public abstract class TreeItemVM<TModel, TViewModel> : TreeItemVMBase
        where TViewModel : TreeItemVM<TModel, TViewModel>
        where TModel : TreeItem<TModel>
    {
        /// <summary>
        /// The strongly-typed wrapped model
        /// </summary>
        protected TModel _internal { get { return (TModel)_item; } }

        /// <summary>
        /// Provides access to errors exposed to the IDataErrorInfo interface.  These errors can be registered with 
        /// the static method AddPropertyValidation
        /// </summary>
        /// <param name="name">The name of the property to retrieve an error for</param>
        /// <returns></returns>
        public override string this[string name]
        {
            get
            {
                Tuple<Predicate<TViewModel>, string> check;
                if (_dataErrorProviders.TryGetValue(name, out check))
                {
                    if (!check.Item1((TViewModel)this))
                        return check.Item2;
                }

                return String.Empty;
            }
        }

        /// <summary>
        /// Stores the validation functions and associated errors if the validation fails
        /// </summary>
        private static Dictionary<string, Tuple<Predicate<TViewModel>, string>> _dataErrorProviders
            = new Dictionary<string, Tuple<Predicate<TViewModel>, string>>();

        /// <summary>
        /// The base constructor that wraps the model
        /// </summary>
        /// <param name="item">The model to wrap</param>
        public TreeItemVM(TModel item)
            : base(item)
        {
        }

        /// <summary>
        /// The constructor to wrap the model and associate with the given parent
        /// </summary>
        /// <param name="parent">The parent of this view model</param>
        /// <param name="item">The model to wrap</param>
        public TreeItemVM(TreeItemVMBase parent, TModel item)
            : base(parent, item)
        {
        }

        /// <summary>
        /// The constructor to wrap the model and the given children
        /// </summary>
        /// <param name="item">The model to wrap</param>
        /// <param name="children">The child view models of this view model</param>
        public TreeItemVM(TModel item, IEnumerable<TreeItemVMBase> children)
            : base(item, children)
        {
        }

        /// <summary>
        /// The constructor to wrap the model and the given children with the provided parent
        /// </summary>
        /// <param name="parent">The parent of this view model</param>
        /// <param name="item">the model to wrap</param>
        /// <param name="children">The child view models of this view model</param>
        public TreeItemVM(TreeItemVMBase parent, TModel item, IEnumerable<TreeItemVMBase> children)
            : base(parent, item, children)
        {
        }

        /// <summary>
        /// Adds a validation function to the specified property.  Replaces the existing validation function if one already exists
        /// </summary>
        /// <param name="property">The property that should be validated using this function</param>
        /// <param name="validator">The predicate to use to validate the selected property</param>
        /// <param name="errMsg">The error message to be displayed if the validator returns false</param>
        protected static void AddPropertyValidation(string property, Predicate<TViewModel> validator, string errMsg)
        {
            _dataErrorProviders[property] = new Tuple<Predicate<TViewModel>, string>(validator, errMsg);
        }
    }
}
