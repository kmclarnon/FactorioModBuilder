using FactorioModBuilder.Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems
{
    public class Project : TreeItem<Project>
    {
        public Project(string name) : base(name)
        {
        }
    }
}
