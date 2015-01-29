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

        public ICommand OpenSelectedItemCmd { get { return this.GetCommand(this.OpenSelectedItem); } }

        public ObservableCollection<IMenuItemProvider> MenuItems { get; private set; }

        public ObservableCollection<SolutionVM> Solutions { get; private set; }

        public SolutionExplorerVM()
        {
            this.MenuItems = new ObservableCollection<IMenuItemProvider>();
            this.Solutions = new ObservableCollection<SolutionVM>();
        }

        private void OpenSelectedItem()
        {

        }
    }
}
