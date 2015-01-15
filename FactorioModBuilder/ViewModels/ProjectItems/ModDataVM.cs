using FactorioModBuilder.Build;
using FactorioModBuilder.Build.Data;
using FactorioModBuilder.Models.ProjectItems;
using FactorioModBuilder.ViewModels.Base;
using FactorioModBuilder.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class ModDataVM : ProjectItem<ModData, ModDataVM>
    {
        public override IEnumerable<DataUnit> CompilerData
        {
            get 
            {
                return new ModDataData().ToList();
            }
        }

        private bool _manualMode;
        public bool ManualMode
        {
            get { return _manualMode; }
            set { this.SetProperty(ref _manualMode, value); }
        }

        public ObservableCollection<ModDataItemVM> DataItems { get; private set; }

        public ModDataVM(ModData data)
            : this(null, data)
        {
        }

        public ModDataVM(TreeItemVMBase parent, ModData data)
            : base(parent, data)
        {
            this.DataItems = new ObservableCollection<ModDataItemVM>();
        }
    }
}
