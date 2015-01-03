using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems
{
    public class Group : ProjectItem
    {
        public string Type { get; set; }
        public string IconPath { get; set; }
        public string InvOrder { get; set; }
        public string Order { get; set; }
        public bool Enabled { get; set; }

        public Group(string name) : base(name)
        {
            this.Enabled = true;
            this.Type = "item-group";
        }
    }
}
