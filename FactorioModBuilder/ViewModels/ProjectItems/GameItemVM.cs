using FactorioModBuilder.Models.ProjectItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class GameItemVM : ProjectItemVM
    {
        public string Type { get { return _internal.Type; } }

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

        public bool Enabled
        {
            get { return _internal.Enabled; }
            set
            {
                if (_internal.Enabled != value)
                {
                    _internal.Enabled = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public string Icon
        {
            get { return _internal.Icon; }
            set
            {
                if (_internal.Icon != value)
                {
                    _internal.Icon = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public int StackSize
        {
            get { return _internal.StackSize; }
            set
            {
                if (_internal.StackSize != value)
                {
                    _internal.StackSize = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        private GameItem _internal { get { return (GameItem)_item; } }

        public GameItemVM(ProjectItemVM parent, GameItem item)
            : base(parent, item)
        {

        }
    }
}
