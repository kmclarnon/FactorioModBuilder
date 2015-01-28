using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfUtils;

namespace FactorioModBuilder.ViewModels.Main
{
    public class MainMenuVM : BaseVM
    {
        public FileMenuVM FileMenu { get; private set; }

        private MainVM _parent;

        public MainMenuVM(MainVM parent, FileMenuVM fileMenu)
        {
            if (parent == null)
                throw new ArgumentNullException("parent");
            if (fileMenu == null)
                throw new ArgumentNullException("fileMenu");

            _parent = parent;
            this.FileMenu = fileMenu;
        }
    }
}
