﻿using FactorioModBuilder.ViewModels.Menu;
using FactorioModBuilder.ViewModels.Menu.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public ObservableCollection<IMenuItemProvider> MenuItems { get; private set; }

        public SolutionExplorerVM()
        {
            this.MenuItems = new ObservableCollection<IMenuItemProvider>();
            this.MenuItems.Add(new ClickableItem("Collapse All", (() => { })));
        }
    }
}
