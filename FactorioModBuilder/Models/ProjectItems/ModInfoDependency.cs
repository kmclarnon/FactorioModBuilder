using FactorioModBuilder.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems
{
    public class ModInfoDependency : TreeItem<ModInfoDependency>
    {
        public bool Optional { get; set; }
        public bool Enabled { get; set; }
        public string Version { get; set; }

        public ModInfoDependency(string name)
            : base(name)
        {
            this.Enabled = true;
        }
    }
}
