using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems
{
    public abstract class ProjectItem
    {
        public string Name { get; set; }

        public ProjectItem(string name)
        {
            this.Name = name;
        }
    }
}
