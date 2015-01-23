using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public ICommand AddFluidCmd { get { return this.GetCommand(this.AddFluid, this.CanAddFluid); } }
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

        private bool CanAddFluid()
        {
            return true;
        }

        private void AddFluid()
        {
            this.ItemList.Add(new FluidVM(
                new Fluid("new-fluid-" + _newCount)));
            _newCount++;
        }

        private bool CanRemoveFluid()
        {
            return this.ItemList.Where(o => o.IsSelected).Any();
        }

        private void RemoveFluid()
        {
            var res = this.ItemList.Where(o => o.IsSelected).ToList();
            foreach (var r in res)
                this.ItemList.Remove(r);
        }
    }
}
