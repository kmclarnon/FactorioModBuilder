using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using FactorioModBuilder.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class FluidsVM : ProjectItem<Fluids, FluidsVM>
    {
        public override IEnumerable<Build.Data.DataUnit> CompilerData
        {
            get { return this.ItemList.SelectMany(o => o.CompilerData); }
        }

        public ObservableCollection<FluidVM> ItemList { get; set; }

        public ICommand AddFluidCmd { get { return this.GetCommand(this.AddFluid); } }
        public ICommand RemoveFluidCmd { get { return this.GetCommand(this.RemoveFluid, this.CanRemoveFluid); } }

        public ObservableCollection<SubGroupVM> PossibleSubGroups
        {
            get
            {
                PrototypesVM res;
                if (!this.TryFindElementUp(out res))
                    throw new Exception("Could not find subgroups view model");
                return res.ItemSubgroups;
            }
        }

        private int _newCount = 1;

        public FluidsVM(Fluids fl) 
            : this(null, fl)
        {
        }

        public FluidsVM(TreeItemVMBase parent, Fluids fl)
            : base(parent, fl)
        {
            this.ItemList = new ObservableCollection<FluidVM>();
        }

        protected override void Initialize()
        {
            this.PossibleSubGroups.CollectionChanged += HandlePossibleSubGroupsChanged;
        }

        private void HandlePossibleSubGroupsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach(SubGroupVM sg in e.OldItems)
                {
                    foreach (var f in this.ItemList)
                        if(f.SubGroupItem == sg)
                            f.ForceRemoveSubGroup();
                }
            }
        }

        /// <summary>
        /// Adds a new fluid to the ItemList collection
        /// </summary>
        private void AddFluid()
        {
            this.ItemList.Add(new FluidVM(
                new Fluid("new-fluid-" + _newCount)));
            _newCount++;
        }

        /// <summary>
        /// Determines if any fluids can be removed from the ItemList collection
        /// </summary>
        /// <returns>True if any fluids are selected, otherwise false</returns>
        private bool CanRemoveFluid()
        {
            return this.ItemList.Where(o => o.IsSelected).Any();
        }

        /// <summary>
        /// Removes all selected fluids from the ItemList collection
        /// </summary>
        private void RemoveFluid()
        {
            this.ItemList.RemoveWhere(o => o.IsSelected);
        }
    }
}
