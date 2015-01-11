using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
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
        public ICommand AddEquipmentCmd { get { return this.GetCommand(this.AddEquipment, this.CanAddEquipment); } }
        public ICommand RemoveEquipmentCmd { get { return this.GetCommand(this.RemoveEquipment, this.CanRemoveEquipment); } }

        public ObservableCollection<EquipmentVM> ItemList { get; private set; }
        public ObservableCollection<String> ShapeTypes { get; private set; }
        public ObservableCollection<String> EnergySourceTypes { get; private set; }
        public ObservableCollection<String> EquipmentTypes { get; private set; }

        private int _newCount = 1;

        public EquipsVM(Equips eq)
            : this(null, eq)
        {
        }

        public EquipsVM(TreeItemVMBase parent, Equips eq)
            : base(parent, eq)
        {
            this.ItemList = new ObservableCollection<EquipmentVM>();
            this.ShapeTypes = new ObservableCollection<string>();
            this.ShapeTypes.Add("full");
            this.EnergySourceTypes = new ObservableCollection<string>();
            this.EnergySourceTypes.Add("electric");
            this.EquipmentTypes = new ObservableCollection<string>();
            this.EquipmentTypes.Add("night-vision-equipment");
            this.EquipmentTypes.Add("energy-shield-equipment");
            this.EquipmentTypes.Add("battery-equipment");
            this.EquipmentTypes.Add("solar-pannel-equipment");
            this.EquipmentTypes.Add("generator-equipment");
            this.EquipmentTypes.Add("active-defense-equipment");
            this.EquipmentTypes.Add("movement-bonus-equipment");
        }

        public bool CanAddEquipment()
        {
            return true;
        }

        public void AddEquipment()
        {
            this.ItemList.Add(new EquipmentVM(this, 
                new Equipment("New Equipment " + _newCount)));
            _newCount++;
        }

        public bool CanRemoveEquipment()
        {
            return this.ItemList.Where(o => o.IsSelected).Any();
        }

        public void RemoveEquipment()
        {
            var lst = this.ItemList.Where(o => o.IsSelected).ToList();
            foreach (var i in lst)
                this.ItemList.Remove(i);
        }
    }
}
