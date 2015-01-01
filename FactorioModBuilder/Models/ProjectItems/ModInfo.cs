using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems
{
    public class ModInfo : ProjectItem
    {
        public string ModName { get; set; }
        public string Version { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Contact { get; set; }
        public string Homepage { get; set; }
        public string Description { get; set; }

        public ModInfo() : base("Mod Info")
        {
        }
    }
}
