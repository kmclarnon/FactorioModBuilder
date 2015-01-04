using FactorioModBuilder.Models.ProjectItems;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class ModDataVM : ProjectItemVM<ModData>
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

        public ModDataVM(ProjectItemVMBase parent, ModData data)
            : base(parent, data)
        {
            this.DataItems = new ObservableCollection<ModDataItemVM>();
        }
    }
}
