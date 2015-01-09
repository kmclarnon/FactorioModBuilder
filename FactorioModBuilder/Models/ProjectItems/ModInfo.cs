using FactorioModBuilder.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems
{
    public class ModInfo : TreeItem<ModInfo>
    {
        public string ModName { get; set; }
        public string Version { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Contact { get; set; }
        public string Homepage { get; set; }
        public string Description { get; set; }

        public ModInfo(string modName) : base("Mod Info")
        {
            this.ModName = modName;
            this.Version = String.Empty;
            this.Title = String.Empty;
            this.Author = String.Empty;
            this.Contact = String.Empty;
            this.Homepage = String.Empty;
            this.Description = String.Empty;
        }
    }
}
