using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfUtils;
using FactorioModBuilder.Models;
using FactorioModBuilder.Models.ProjectItems;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class ProjectItemVM : BaseVM
    {
        private bool _isExpanded;
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if(_isExpanded != value)
                {
                    _isExpanded = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if(_isSelected != value)
                {
                    _isSelected = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public string Name
        {
            get { return _item.Name; }
            set
            {
                if(_item.Name != value)
                {
                    _item.Name = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public ObservableCollection<ProjectItemVM> Children { get; private set; }

        protected ProjectItem _item;
        protected ProjectItemVM _parent;

        public ProjectItemVM(ProjectItemVM parent, ProjectItem item)
        {
            _item = item;
            _parent = parent;
            this.Children = new ObservableCollection<ProjectItemVM>();
            foreach(var c in _item.Children)
                this.Children.Add(ProjectItemVM.Wrap(this, c));
        }

        public bool TryFindElementUp<T>(out T element) where T : ProjectItemVM
        {
            element = default(T);
            T res = (T)_parent;
            while(res != null)
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

        public bool TryFindElementDown<T>(out T element) where T : ProjectItemVM
        {
            element = default(T);
            foreach(var c in this.Children)
            {
                if(c.GetType() == typeof(T))
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
            string propName, out ProjectItemVM element)
        {
            element = null;
            ProjectItemVM res = _parent;
            while(res != null)
            {
                var type = res.GetType();
                foreach(var p in type.GetProperties())
                {
                    if(p.PropertyType == propType && p.Name == propName)
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
            string propName, out ProjectItemVM element)
        {
            element = null;
            foreach(var c in this.Children)
            {
                var type = c.GetType();
                foreach(var p in type.GetProperties())
                {
                    if(p.PropertyType == propType && p.Name == propName)
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

        private static readonly Dictionary<Type, Func<ProjectItemVM, ProjectItem, ProjectItemVM>> _wrapDict =
            new Dictionary<Type, Func<ProjectItemVM, ProjectItem, ProjectItemVM>>()
        {
            { typeof(ModControl),       ((x, y) => new ModControlVM(x, (ModControl)y)) },
            { typeof(ModData),          ((x, y) => new ModDataVM(x, (ModData)y)) },
            { typeof(ModInfo),          ((x, y) => new ModInfoVM(x, (ModInfo)y)) },
            { typeof(ProjectHeader),    ((x, y) => new ProjectHeaderVM((ProjectHeader)y)) },
            { typeof(Prototypes),       ((x, y) => new PrototypesVM(x, (Prototypes)y)) },
            { typeof(Groups),       ((x, y) => new GroupsVM(x, (Groups)y)) },
            { typeof(SubGroups),    ((x, y) => new SubGroupsVM(x, (SubGroups)y)) },
            { typeof(GameItems),            ((x, y) => new GameItemsVM(x, (GameItems)y)) } 
        };

        public static ProjectItemVM Wrap(ProjectItemVM parent, ProjectItem item)
        {
            if (item == null)
                throw new ArgumentNullException("Wrapped item cannot be null");

            return _wrapDict[item.GetType()](parent, item);
        }
    }
}
