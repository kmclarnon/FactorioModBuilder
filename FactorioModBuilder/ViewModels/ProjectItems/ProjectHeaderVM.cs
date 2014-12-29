using FactorioModBuilder.Models.ProjectItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class ProjectHeaderVM : ProjectItemVM
    {
        public ProjectHeaderVM(ProjectHeader header)
            : base(null, header)
        {
        }
    }
}
