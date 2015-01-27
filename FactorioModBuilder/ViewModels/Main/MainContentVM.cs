using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfUtils;

namespace FactorioModBuilder.ViewModels.Main
{
    public class MainContentVM : BaseVM
    {
        public ObservableCollection<TreeItemVMBase> Content { get; private set; }

        public MainContentVM()
        {
            this.Content = new ObservableCollection<TreeItemVMBase>();
        }
    }
}
