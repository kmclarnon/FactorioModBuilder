using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfUtils;
using FactorioModBuilder.Models.ProjectItems;
using FactorioModBuilder.ViewModels.ProjectItems;

namespace FactorioModBuilder.ViewModels
{
    public class ProjectVM : BaseVM
    {
        public List<ProjectItemVM> ProjectItems { get; private set; }

        private Project _project;

        public ProjectVM(Project project)
        {
            _project = project;
            this.ProjectItems = new List<ProjectItemVM>() { new ProjectHeaderVM(_project.ProjectItem) };
        }
    }
}
