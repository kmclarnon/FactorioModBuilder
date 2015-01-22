using FactorioModBuilder.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems
{
    [DataContract]
    public class ModInfo : TreeItem<ModInfo>
    {
        [DataMember]
        public string ModName { get; set; }
        [DataMember]
        public string Version { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Author { get; set; }
        [DataMember]
        public string Contact { get; set; }
        [DataMember]
        public string Homepage { get; set; }
        [DataMember]
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
