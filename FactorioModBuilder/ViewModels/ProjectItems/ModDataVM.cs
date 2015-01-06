using FactorioModBuilder.Models.ProjectItems;
using FactorioModBuilder.ViewModels.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class ModDataVM : TreeItemVM<ModData>
    {
        private bool _manualMode;
        public bool ManualMode
        {
            get { return _manualMode; }
            set
            {
                if(_manualMode != value)
                {
                    _manualMode = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public ObservableCollection<ModDataItemVM> DataItems { get; private set; }

        public ModDataVM(TreeItemVMBase parent, ModData data)
            : base(parent, data)
        {
            this.DataItems = new ObservableCollection<ModDataItemVM>();
        }
    }
}
