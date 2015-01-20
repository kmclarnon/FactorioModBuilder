using FactorioModBuilder.Models.ProjectItems;
using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.Models.Base;
using FactorioModBuilder.ViewModels.ProjectItems;
using FactorioModBuilder.ViewModels.ProjectItems.Prototype;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfUtils;
using System.ComponentModel;

namespace FactorioModBuilder.ViewModels.Base
{
    public abstract class TreeItemVMBase : BaseVM, IDataErrorInfo
    {
        public delegate void InitCompleteHandler(TreeItemVMBase sender, EventArgs e);
        public event InitCompleteHandler OnInitComplete;

        protected TreeItemVMBase _parent;

        public ObservableCollection<TreeItemVMBase> Children { get; private set; }

        private bool _isExpanded;
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (_isExpanded != value)
                {
                    _isExpanded = value;
                    this.NotifyPropertyChanged();
                    // if we are expanded, expand our parent
                    if (_isExpanded && _parent != null)
                        _parent.IsExpanded = true;
                }
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    this.NotifyPropertyChanged();
                    this.OnIsSelected();
                }
            }
        }

        public string Name
        {
            get { return this.OnGetName(_item.Name); }
            set
            {
                if (_item.Name != value)
                {
                    _item.Name = value;
                    this.NotifyPropertyChanged();
                    this.OnSetName();
                }
            }
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public abstract string this[string name] { get; }

        protected TreeItemBase _item;

        private bool _initOnce = false;

        public TreeItemVMBase(TreeItemBase item)
            : this(null, item)
        {
        }

        public TreeItemVMBase(TreeItemVMBase parent, TreeItemBase item)
            : this(parent, item, new List<TreeItemVMBase>())
        {
        }

        public TreeItemVMBase(TreeItemBase item, IEnumerable<TreeItemVMBase> children)
            : this(null, item, children)
        {
        }

        public TreeItemVMBase(TreeItemVMBase parent, TreeItemBase item, IEnumerable<TreeItemVMBase> children)
        {
            _parent = parent;
            _item = item;
            this.Children = new ObservableCollection<TreeItemVMBase>();
            foreach (var c in children)
            {
                c._parent = this;
                this.OnInitComplete += c.parent_OnInitComplete;
                this.Children.Add(c);
            }

            if(_parent != null)
                parent.OnInitComplete += parent_OnInitComplete;
        }

        private void parent_OnInitComplete(TreeItemVMBase sender, EventArgs e)
        {
            this.Initialize();
            this.InitComplete();
        }

        protected virtual void Initialize()
        {

        }

        protected void InitComplete()
        {
            if(!_initOnce && this.OnInitComplete != null)
            {
                this.OnInitComplete(this, new EventArgs());
                _initOnce = true;
            }
        }

        protected virtual string OnGetName(string name)
        {
            return name;
        }

        protected virtual void OnSetName()
        {
        }

        protected virtual void OnIsSelected()
        {
        }

        public void ExpandDown()
        {
            this.IsExpanded = true;
            foreach (var c in this.Children)
                c.ExpandDown();
        }

        public void ExpandDown(int levels)
        {
            if (levels < 1)
                return;
            this.IsExpanded = true;
            foreach (var c in this.Children)
                c.ExpandDown(levels - 1);
        }

        public bool TryFindElementUp<T>(out T element) where T : TreeItemVMBase
        {
            element = default(T);
            var res = (TreeItemVMBase)_parent;
            while (res != null)
            {
                if (res.GetType() == typeof(T))
                {
                    element = (T)res;
                    return true;
                }
                else
                    res = (TreeItemVMBase)res._parent;
            }

            return false;
        }

        public bool TryFindElementDown<T>(out T element) where T : TreeItemVMBase
        {
            element = default(T);
            foreach (var c in this.Children)
            {
                if (c.GetType() == typeof(T))
                {
                    element = (T)c;
                    return true;
                }

                if (c.TryFindElementDown<T>(out element))
                    return true;
            }

            return false;
        }

        public bool TryFindElementPeer<T>(out T element) where T : TreeItemVMBase
        {
            element = default(T);
            TreeItemVMBase res = (TreeItemVMBase)_parent;
            if (res == null)
                return false;
            else
            {
                foreach(var c in res.Children)
                {
                    if(c.GetType() == typeof(T) && !ReferenceEquals(c, this))
                    {
                        element = (T)c;
                        return true;
                    }
                }
            }

            return true;
        }
    }
}
