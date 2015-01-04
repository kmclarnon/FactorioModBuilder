using FactorioModBuilder.Models.ProjectItems;
using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.ProjectItems.Prototype;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfUtils;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public abstract class ProjectItemVMBase : BaseVM
    {
        protected ProjectItemVMBase _parent;

        public ObservableCollection<ProjectItemVMBase> Children { get; private set; }

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
            get { return _item.Name; }
            set
            {
                if (_item.Name != value)
                {
                    _item.Name = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        protected ProjectItemBase _item;

        public ProjectItemVMBase(ProjectItemVMBase parent, ProjectItemBase item)
        {
            _item = item;
            _parent = parent;
            this.Children = new ObservableCollection<ProjectItemVMBase>();
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

        public bool TryFindElementUp<T>(out T element) where T : ProjectItemVMBase
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

        public bool TryFindElementDown<T>(out T element) where T : ProjectItemVMBase
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
            string propName, out ProjectItemVMBase element)
        {
            element = null;
            ProjectItemVMBase res = _parent;
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
            string propName, out ProjectItemVMBase element)
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

        private static readonly Dictionary<Type, Func<ProjectItemVMBase, ProjectItemBase, ProjectItemVMBase>> _wrapDict =
            new Dictionary<Type, Func<ProjectItemVMBase, ProjectItemBase, ProjectItemVMBase>>()
        {
            { typeof(ModControl),       ((x, y) => new ModControlVM(x, (ModControl)y)) },
            { typeof(ModData),          ((x, y) => new ModDataVM(x, (ModData)y)) },
            { typeof(ModInfo),          ((x, y) => new ModInfoVM(x, (ModInfo)y)) },
            { typeof(ProjectHeader),    ((x, y) => new ProjectHeaderVM((ProjectHeader)y)) },
            { typeof(Prototypes),       ((x, y) => new PrototypesVM(x, (Prototypes)y)) },
            { typeof(Groups),           ((x, y) => new GroupsVM(x, (Groups)y)) },
            { typeof(SubGroups),        ((x, y) => new SubGroupsVM(x, (SubGroups)y)) },
            { typeof(Items),            ((x, y) => new ItemsVM(x, (Items)y)) },
            { typeof(Recipe),           ((x, y) => new RecipeVM(x, (Recipe)y)) },
            { typeof(Technology),       ((x, y) => new TechnologyVM(x, (Technology)y)) },
            { typeof(Tile),             ((x, y) => new TileVM(x, (Tile)y)) }
        };

        public static ProjectItemVMBase Wrap(ProjectItemVMBase parent, ProjectItemBase item)
        {
            if (item == null)
                throw new ArgumentNullException("Wrapped item cannot be null");

            return _wrapDict[item.GetType()](parent, item);
        }
    }
}
