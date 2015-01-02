using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems
{
    public class SubGroup : ProjectItem
    {
        public string Type { get; set; }
        public string Group { get; set; }
        public string Order { get; set; }
        public bool Enabled { get; set; }

        public SubGroup(string name) : base(name)
        {
        }
    }
}
