using FactorioModBuilder.ViewModels.Base;
using FactorioModBuilder.ViewModels.Menu;
using FactorioModBuilder.ViewModels.Menu.Base;
using FactorioModBuilder.ViewModels.ProjectItems;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfUtils;

namespace FactorioModBuilder.ViewModels
{
    public class SolutionExplorerVM : BaseVM, IMenuProvider
    {
        public string SearchText
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
        }

        public string SearchWatermark { get { return "Search Solution Explorer (Ctrl+;)"; } }

        public bool CaseSensitive
        {
            get { return this.GetProperty<bool>(); }
            set { this.SetProperty(value); }
        }

        public bool SearchExtern
        {
            get { return this.GetProperty<bool>(); }
            set { this.SetProperty(value); }
        }

        public ICommand OpenCmd { get { return this.GetCommand(this.Open); } }

        public ICommand RenameCmd { get { return this.GetCommand(this.RenameSelected); } }

        public ObservableCollection<IMenuItemProvider> MenuItems { get; private set; }

        public ObservableCollection<SolutionVM> Solutions { get; private set; }

        private MainVM _parent;

        public SolutionExplorerVM(MainVM parent)
        {
            if (parent == null)
                throw new ArgumentNullException("parent");
            _parent = parent;
            this.MenuItems = new ObservableCollection<IMenuItemProvider>();
            this.Solutions = new ObservableCollection<SolutionVM>();
        }

        private void Open()
        {
            var res = this.Solutions.SelectMany(o => o.SelectedNodes)
                .Where(o => o is IDoubleClickBehavior).Cast<IDoubleClickBehavior>();
            var open = res.Where(o => o.DoubleClickBehavior == DoubleClickBehavior.OpenContent);
            var openParent = res.Where(o => o.DoubleClickBehavior == DoubleClickBehavior.OpenParent);

            _parent.OpenItems(open.Cast<TreeItemVMBase>());
            _parent.OpenParentItems(openParent.Cast<TreeItemVMBase>());
        }

        private void RenameSelected()
        {
            var res = this.Solutions.SelectMany(o => o.SelectedNodes);
            if (res.Any())
                res.First().DoRename();
        }
    }
}
