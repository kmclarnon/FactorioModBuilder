using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems
{
    public class ModData : ProjectItem
    {
        public bool ManualMode { get; set; }
        public List<ModDataItem> DataItems { get; private set; }

        public ModData() : base("Mod Data")
        {
            this.DataItems = new List<ModDataItem>();
        }
    }
}
