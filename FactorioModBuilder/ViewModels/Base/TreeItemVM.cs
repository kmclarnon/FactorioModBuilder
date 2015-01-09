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
    public abstract class TreeItemVM<T, U> : TreeItemVMBase
        where U : TreeItemVM<T, U>
        where T : TreeItem<T>
    {
        protected T _internal { get { return (T)_item; } }

        public override string this[string name]
        {
            get
            {
                Tuple<Func<U, bool>, string> check;
                if (_dataErrorProviders.TryGetValue(name, out check))
                {
                    if (!check.Item1((U)this))
                        return check.Item2;
                }

                return String.Empty;
            }
        }

        private static Dictionary<string, Tuple<Func<U, bool>, string>> _dataErrorProviders
            = new Dictionary<string, Tuple<Func<U, bool>, string>>();

        public TreeItemVM(T item)
            : base(item)
        {
        }

        public TreeItemVM(TreeItemVMBase parent, T item)
            : base(parent, item)
        {
        }

        public TreeItemVM(T item, IEnumerable<TreeItemVMBase> children)
            : base(item, children)
        {
        }

        public TreeItemVM(TreeItemVMBase parent, T item, IEnumerable<TreeItemVMBase> children)
            : base(parent, item, children)
        {
        }

        protected static void AddPropertyValidation(string property, Func<U, bool> validator, string errMsg)
        {
            if(!_dataErrorProviders.ContainsKey(property))
                _dataErrorProviders.Add(property, 
                    new Tuple<Func<U, bool>, string>(validator, errMsg));
        }
    }
}
