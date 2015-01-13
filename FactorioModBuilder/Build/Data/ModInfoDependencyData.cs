using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Data
{
    [DataContract]
    public class ModInfoDependencyData : DataUnit
    {
        [DataMember]
        public string Value { get; private set; }

        public ModInfoDependencyData(string name, string version, bool optional)
            : base(Extensions.ExtensionType.FactorioDependencies)
        {
            this.Value = String.Empty;
            if (optional)
                this.Value = "? ";
            this.Value += name;
            this.Value += version;
        }
    }
}
