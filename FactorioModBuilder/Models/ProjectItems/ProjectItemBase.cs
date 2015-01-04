using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems
{
    public abstract class ProjectItemBase
    {
        public string Name { get; set; }

        public ProjectItemBase(string name)
        {
            this.Name = name;
        }
    }
}
