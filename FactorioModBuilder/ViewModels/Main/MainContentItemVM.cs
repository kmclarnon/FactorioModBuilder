using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfUtils;

namespace FactorioModBuilder.ViewModels.Main
{
    public class MainContentItemVM : BaseVM
    {
        public string HeaderText
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
        }

        public bool IsSelected
        {
            get { return this.GetProperty<bool>(); }
            set { this.SetProperty(value); }
        }

        public TreeItemVMBase ItemContent { get; private set; }

        public MainContentItemVM(TreeItemVMBase item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            this.ItemContent = item;
            this.HeaderText = this.ItemContent.Name;
            this.ItemContent.PropertyChanged += ItemPropertyChanged;
        }

        void ItemPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.HeaderText = ItemContent.Name;
        }
    }
}
