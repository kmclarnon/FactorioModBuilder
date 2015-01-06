using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfUtils;

namespace FactorioModBuilder.Models
{
    public class Solution
    {
        public List<Project> Projects { get; private set; }
        public string Name { get; set; }

        public Solution(string name, Project p)
        {
            this.Name = name;
            this.Projects = new List<Project>();
        }

        public Solution(string name, IEnumerable<Project> projects)
        {
            this.Name = name;
            this.Projects = projects.ToList();
        }
    }
}
