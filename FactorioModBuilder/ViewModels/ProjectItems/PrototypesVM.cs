using FactorioModBuilder.Models.ProjectItems;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class PrototypesVM : ProjectItemVM
    {
        public ObservableCollection<String> PossibleSubgroups
        {
            get
            {
                ProjectItemVM res;
                if (!this.TryFindElementWithPropertyDown(typeof(ObservableCollection<String>),
                    "SubgroupNames", out res))
                {
                    throw new Exception("Could not find appropriate child element to populate Possible Subgroups");
                }

                return (ObservableCollection<String>)res.GetType()
                    .GetProperty("SubgroupNames").GetValue(res);
            }
        }

        public PrototypesVM(ProjectItemVM parent, Prototypes types)
            : base(parent, types)
        {
        }
    }
}
