using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FactorioModBuilder.Models.ProjectItems;

namespace FactorioModBuilder
{
    public class Project
    {
        public ProjectHeader ProjectItem { get; private set; }

        public Project(string projectName)
        {
            this.ProjectItem = new ProjectHeader(projectName);
        }
    }
}
