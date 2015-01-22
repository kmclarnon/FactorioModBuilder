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
    public class Project : TreeItem<Project>
    {
        [DataMember]
        public string TempDir { get; set; }
        [DataMember]
        public string OutDir { get; set; }
        [DataMember]
        public string Version { get; set; }

        public Project(string name, string tmpdir, 
            string outdir) : base(name)
        {
            this.TempDir = tmpdir;
            this.OutDir = outdir;
        }
    }
}
