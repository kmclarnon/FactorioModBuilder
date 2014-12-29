using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfUtils;

namespace FactorioModBuilder
{
    public class ProjectItemVM : BaseVM
    {
        private bool _isExpanded;
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if(_isExpanded != value)
                {
                    _isExpanded = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if(_isSelected != value)
                {
                    _isSelected = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if(_name != value)
                {
                    _name = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
    }
}
