using FactorioModBuilder.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems.Prototype
{
    public class Technology : TreeItem<Technology>
    {
        public string IconPath { get; set; }

        public int UnitCount { get; set; }
        public int UnitTime { get; set; }

        public string Order { get; set; }
        public bool Upgrade { get; set; }

        public Technology(string name) : base(name)
        {
        }
    }
}
