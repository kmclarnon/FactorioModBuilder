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

        public TreeItemVMBase Content { get; private set; }

        public MainContentItemVM(TreeItemVMBase item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            this.Content = item;
            this.HeaderText = this.Content.Name;
            this.Content.PropertyChanged += ItemPropertyChanged;
        }

        void ItemPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.HeaderText = Content.Name;
        }
    }
}
