using FactorioModBuilder.Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems
{
    public class ModDataItem : TreeItem<ModDataItem>
    {
        public string Type { get; set; }
        public bool Required { get; set; }

        public ModDataItem(string name) : base(name)
        {
        }
    }
}
