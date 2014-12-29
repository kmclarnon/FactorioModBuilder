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

        public ModInfo(string name, string version, string title,
            string author, string contact, string homepage,
            string description)
            : base("Mod Info")
        {
            this.ModName = name;
            this.Version = version;
            this.Title = title;
            this.Author = author;
            this.Contact = contact;
            this.Homepage = homepage;
            this.Description = description;
        }
    }
}
