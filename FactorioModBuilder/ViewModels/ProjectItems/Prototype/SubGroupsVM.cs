using FactorioModBuilder.Build;
using FactorioModBuilder.Build.Data;
using FactorioModBuilder.Build.Extensions;
using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfUtils;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class SubGroupsVM : ProjectItem<SubGroups, SubGroupsVM>
    {
        public ObservableCollection<SubGroupVM> ItemList { get; private set; }

        public override DataUnit CompilerData
        {
            get
            {
                return new SubGroupsData(this.ItemList
                    .Select(o => (SubGroupData)o.CompilerData));
            }
        }

        public ObservableCollection<GroupVM> PossibleGroups
        {
            get
            {
                PrototypesVM res;
                if (!this.TryFindElementUp<PrototypesVM>(out res))
                    throw new Exception("Failed to find parent to supply PossibleGroups");
                return res.PossibleGroups;
            }
        }

        public ICommand AddSubgroupCmd { get { return this.GetCommand(this.AddSubgroup, this.CanAddSubgroup); } }
        public ICommand RemoveSubgroupCmd { get { return this.GetCommand(this.RemoveSubgroup, this.CanRemoveSubgroup); } }

        private int _newCount = 1;

        public SubGroupsVM(SubGroups items)
            : this(null, items)
        {
        }

        protected override void Initialize()
        {
            this.PossibleGroups.CollectionChanged += HandlePossibleGroupsChanged;
        }

        private void HandlePossibleGroupsChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // the only case we are about is the removal of a group.  Typically this
            // would be handled by the databinding seamlessly, however there is a bug
            // with comboboxes and the selectedItem binding being set to null when
            // the visual tree changes (which happens whenever a user selects a different
            // screen in this app.  The only way to prevent this is to by default disallow 
            // null in the Group property setter of the SubGroupVM.  However this becomes a
            // problem when the selected group is removed from the list of possible groups. 
            // Visually it looks correct to the user but the Group (and subsequently the GroupName)
            // property still contain the group that has been removed.  In order to work around this
            // we use this event handler to detect those changes and force the value to be null

            if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                foreach (GroupVM g in e.OldItems)
                {
                    foreach (var sg in this.ItemList)
                    {
                        if (sg.Group == g)
                            sg.ForceRemoveGroup();
                    }
                }
            }
        }

        public SubGroupsVM(TreeItemVMBase parent, SubGroups items)
            : base(parent, items)
        {
            this.ItemList = new ObservableCollection<SubGroupVM>();
        }

        private bool CanAddSubgroup()
        {
            return true;
        }

        private void AddSubgroup()
        {
            this.ItemList.Add(new SubGroupVM(this, 
                new SubGroup("New Subgroup " + _newCount)));
            _newCount++;
        }

        private bool CanRemoveSubgroup()
        {
            return this.ItemList.Where(o => o.IsSelected).Any();
        }

        private void RemoveSubgroup()
        {
            var list = this.ItemList.Where(o => o.IsSelected).ToList();
            foreach (var i in list)
                this.ItemList.Remove(i);
        }
    }
}
