using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems
{
    public class ModDataItem : ProjectItem<ModDataItem>
    {
        public string Type { get; set; }
        public bool Required { get; set; }

        public ModDataItem(string name) : base(name)
        {
        }
    }
}
