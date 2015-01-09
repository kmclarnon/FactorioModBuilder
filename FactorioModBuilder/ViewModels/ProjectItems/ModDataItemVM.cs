using FactorioModBuilder.Models.ProjectItems;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class ModDataItemVM : TreeItemVM<ModDataItem, ModDataItemVM>
    {
        public string Type
        {
            get { return _data.Type; }
            set
            {
                if(_data.Type != value)
                {
                    _data.Type = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public bool Required
        {
            get { return _data.Required; }
            set
            {
                if(_data.Required != value)
                {
                    _data.Required = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        private ModDataItem _data { get { return (ModDataItem)_item; } }

        public ModDataItemVM(ModDataItem item)
            : base(item)
        {
        }

        public ModDataItemVM(TreeItemVMBase parent, ModDataItem item)
            : base(parent, item)
        {
        }
    }
}
