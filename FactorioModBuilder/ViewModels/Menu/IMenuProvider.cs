using FactorioModBuilder.ViewModels.Menu.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels
{
    /// <summary>
    /// Interface for objects that expose a collectin of menu items
    /// </summary>
    public interface IMenuProvider
    {
        ObservableCollection<IMenuItemProvider> MenuItems { get; }
    }
}
