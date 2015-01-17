using FactorioModBuilder.Build.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Data
{
    public class ModInfoData : DataUnit
    {
        public string Name { get; private set; }
        public string Version { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public string Contact { get; private set; }
        public string Homepage { get; private set; }
        public string Description { get; private set; }
        
        public ModInfoData(string name, string version, string title, string author, 
            string contact, string homepage, string description)
            : base(ExtensionType.FactorioInfo)
        {
            this.Name = name;
            this.Version = version;
            this.Title = title;
            this.Author = author;
            this.Contact = contact;
            this.Homepage = homepage;
            this.Description = description;
        }
    }
}
