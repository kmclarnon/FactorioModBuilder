using FactorioModBuilder.Models.ProjectItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class ModInfoVM : ProjectItemVM
    {
        public string ModName
        {
            get { return _mItem.ModName; }
            set
            {
                if(_mItem.ModName != value)
                {
                    _mItem.ModName = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        private ModInfo _mItem { get { return (ModInfo)_item; } }

        public ModInfoVM(ProjectItemVM parent, ModInfo info)
            : base(parent, info)
        {

        }
    }
}
