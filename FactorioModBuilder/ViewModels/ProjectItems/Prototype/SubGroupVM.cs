using FactorioModBuilder.Models.ProjectItems.Prototype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class SubGroupVM : ProjectItemVM
    {
        public string Type
        {
            get { return _internal.Type; }
            set
            {
                if(_internal.Type != value)
                {
                    _internal.Type = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public string Group
        {
            get { return _internal.Group; }
            set
            {
                if(_internal.Group != value)
                {
                    _internal.Group = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public string Order
        {
            get { return _internal.Order; }
            set
            {
                if(_internal.Order != value)
                {
                    _internal.Order = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        private SubGroup _internal { get { return (SubGroup)_item; } }

        public SubGroupVM(ProjectItemVM parent, SubGroup item)
            : base(parent, item)
        {
        }
    }
}
