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
        internal string name;
        [DataMember]
        internal string version;
        [DataMember]
        internal string title;
        [DataMember]
        internal string author;
        [DataMember]
        internal string contact;
        [DataMember]
        internal string homepage;
        [DataMember]
        internal string description;
        
        public ModInfoData(string name, string version, string title, string author, string contact, string homepage, string description)
        {
            this.name = name;
            this.version = version;
            this.title = title;
            this.author = author;
            this.contact = contact;
            this.homepage = homepage;
            this.description = description;
        }
    }
}
