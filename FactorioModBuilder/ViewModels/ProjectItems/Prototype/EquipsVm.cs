using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using FactorioModBuilder.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfUtils;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class EquipsVM : TreeItemVM<Equips, EquipsVM>
    {
        public ICommand AddEquipmentCmd { get { return this.GetCommand(this.AddEquipment); } }
        public ICommand RemoveEquipmentCmd { get { return this.GetCommand(this.RemoveEquipment, this.CanRemoveEquipment); } }

        public ObservableCollection<EquipmentVM> ItemList { get; private set; }

        private int _newCount = 1;

        public EquipsVM(Equips eq)
            : this(null, eq)
        {
        }

        public EquipsVM(TreeItemVMBase parent, Equips eq)
            : base(parent, eq)
        {
            this.ItemList = new ObservableCollection<EquipmentVM>();
        }

        /// <summary>
        /// Adds a new equipment to the ItemList collection
        /// </summary>
        public void AddEquipment()
        {
            this.ItemList.Add(new EquipmentVM(this, 
                new Equipment("New Equipment " + _newCount)));
            _newCount++;
        }

        /// <summary>
        /// Determines if any equipment can be removed from the ItemList collection
        /// </summary>
        /// <returns>True if any equipment are selected, otherwise false</returns>
        public bool CanRemoveEquipment()
        {
            return this.ItemList.Any(o => o.IsSelected);
        }

        /// <summary>
        /// Removes all selected equipment from the ItemList collection
        /// </summary>
        public void RemoveEquipment()
        {
            this.ItemList.RemoveWhere(o => o.IsSelected);
        }
    }
}
