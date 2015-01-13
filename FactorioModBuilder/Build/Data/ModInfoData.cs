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
        [DataMember]
        public string name { get; private set; }
        [DataMember]
        public string version { get; private set; }
        [DataMember]
        public string title { get; private set; }
        [DataMember]
        public string author { get; private set; }
        [DataMember]
        public string contact { get; private set; }
        [DataMember]
        public string homepage { get; private set; }
        [DataMember]
        public string description { get; private set; }
        [DataMember]
        public List<string> dependencies { get; private set; }
        
        public ModInfoData(string name, string version, string title, string author, 
            string contact, string homepage, string description,
            List<ModInfoDependencyData> dependencies)
            : base(Extensions.ExtensionType.FactorioInfo)
        {
            this.name = name;
            this.version = version;
            this.title = title;
            this.author = author;
            this.contact = contact;
            this.homepage = homepage;
            this.description = description;
            this.dependencies = dependencies.Select(o => o.Value).ToList();
        }
    }
}
