using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Data
{
    [DataContract]
    public class ModInfoData : DataUnit
    {
        [DataMember(Name = "name")]
        public string Name { get; private set; }
        [DataMember(Name = "version")]
        public string Version { get; private set; }
        [DataMember(Name = "title")]
        public string Title { get; private set; }
        [DataMember(Name = "author")]
        public string Author { get; private set; }
        [DataMember(Name = "contact")]
        public string Contact { get; private set; }
        [DataMember(Name = "homepage")]
        public string Homepage { get; private set; }
        [DataMember(Name = "description")]
        public string Description { get; private set; }
        [DataMember(Name = "dependencies")]
        public List<string> Dependencies { get; private set; }
        
        public ModInfoData(string name, string version, string title, string author, 
            string contact, string homepage, string description,
            IEnumerable<ModInfoDependencyData> dependencies)
            : base(Extensions.ExtensionType.FactorioInfo)
        {
            this.Name = name;
            this.Version = version;
            this.Title = title;
            this.Author = author;
            this.Contact = contact;
            this.Homepage = homepage;
            this.Description = description;
            this.Dependencies = dependencies.Select(o => o.Value).ToList();
        }
    }
}
