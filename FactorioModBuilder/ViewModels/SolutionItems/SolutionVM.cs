using FactorioModBuilder.Models;
using FactorioModBuilder.Models.SolutionItems;
using FactorioModBuilder.ViewModels.ProjectItems;
using FactorioModBuilder.ViewModels.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfUtils;

namespace FactorioModBuilder.ViewModels
{
    public class SolutionVM : TreeItemVM<Solution>
    {      
        public SolutionVM(Solution sol)
            : base(null, sol)
        {

        }
    }
}
