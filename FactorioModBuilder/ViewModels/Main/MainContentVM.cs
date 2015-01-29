using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfUtils;

namespace FactorioModBuilder.ViewModels.Main
{
    public class MainContentVM : BaseVM
    {
        public ObservableCollection<MainContentItemVM> Content { get; private set; }

        public ICommand CloseTabCmd { get { return this.GetCommand(this.CloseTab); } }

        public MainContentVM()
        {
            this.Content = new ObservableCollection<MainContentItemVM>();
        }

        public void OpenItems(IEnumerable<TreeItemVMBase> items)
        {
            foreach (var i in items)
            {
                var res = this.Content.Where(o => o.Content.Equals(i));
                if (!res.Any())
                {
                    var mc = new MainContentItemVM(i);
                    mc.IsSelected = true;
                    this.Content.Add(mc);
                }
                else
                    res.Single().IsSelected = true;
            }
        }

        private void CloseTab(object param)
        {
            var res = this.Content.Where(o => o.IsSelected).ToList();
            foreach (var r in res)
                this.Content.Remove(r);
        }
    }
}
