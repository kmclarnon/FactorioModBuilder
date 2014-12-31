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
