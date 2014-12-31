using FactorioModBuilder.Models.ProjectItems;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class ModDataVM : ProjectItemVM
    {
        public bool ManualMode
        {
            get { return _data.ManualMode; }
            set
            {
                if(_data.ManualMode != value)
                {
                    _data.ManualMode = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public ObservableCollection<ModDataItemVM> DataItems { get; private set; }

        private ModData _data { get { return (ModData)_item; } }

        public ModDataVM(ProjectItemVM parent, ModData data)
            : base(parent, data)
        {
            this.DataItems = new ObservableCollection<ModDataItemVM>();
            foreach (var c in _data.DataItems)
                this.DataItems.Add(new ModDataItemVM(this, c));
        }
    }
}
