using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfUtils;
using FactorioModBuilder.Models.ProjectItems;
using FactorioModBuilder.ViewModels.Utility;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class ProjectVM : BaseVM
    {
        public List<TreeItemVMBase> ProjectItems { get; private set; }

        private Project _project;

        public ProjectVM(Project project)
        {
            _project = project;
            this.ProjectItems = new List<TreeItemVMBase>() { new ProjectHeaderVM(_project.ProjectItem) };
        }
    }
}
