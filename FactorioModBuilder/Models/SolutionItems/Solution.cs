using FactorioModBuilder.Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfUtils;

namespace FactorioModBuilder.Models.SolutionItems
{
    public class Solution : TreeItem<Solution>
    {
        public List<Project> Projects { get; private set; }

        public Solution(string name, Project p)
            : base(name)
        {
            this.Projects = new List<Project>();
        }

        public Solution(string name, IEnumerable<Project> projects)
            : base(name)
        {
            this.Projects = projects.ToList();
        }
    }
}
