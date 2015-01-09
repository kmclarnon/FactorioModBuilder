﻿using FactorioModBuilder.Models.ProjectItems;
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
                }
            }
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public abstract string this[string name] { get; }

        protected TreeItemBase _item;

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
                this.Children.Add(c);
            }
        }

        protected virtual string OnGetName(string name)
        {
            return name;
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
            T res = (T)_parent;
            while (res != null)
            {
                if (res.GetType() == typeof(T))
                {
                    element = res;
                    return true;
                }
                else
                    res = (T)res._parent;
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

        public bool TryFindElementWithPropertyUp(Type propType,
            string propName, out TreeItemVMBase element)
        {
            element = null;
            TreeItemVMBase res = _parent;
            while (res != null)
            {
                var type = res.GetType();
                foreach (var p in type.GetProperties())
                {
                    if (p.PropertyType == propType && p.Name == propName)
                    {
                        element = res;
                        return true;
                    }
                }

                res = res._parent;
            }

            return false;
        }

        public bool TryFindElementWithPropertyDown(Type propType,
            string propName, out TreeItemVMBase element)
        {
            element = null;
            foreach (var c in this.Children)
            {
                var type = c.GetType();
                foreach (var p in type.GetProperties())
                {
                    if (p.PropertyType == propType && p.Name == propName)
                    {
                        element = c;
                        return true;
                    }
                }

                if (c.TryFindElementWithPropertyDown(propType, propName, out element))
                    return true;
            }

            return false;
        }
    }
}
