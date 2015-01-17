using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Data
{
    public class ModInfoDependencyData : DataUnit
    {
        public string Name { get; private set; }
        public string Version { get; private set; }
        public bool Optional { get; private set; }

        public ModInfoDependencyData(string name, string version, bool optional)
            : base(Extensions.ExtensionType.FactorioDependencies)
        {
            this.Name = name;
            this.Version = version;
            this.Optional = optional;
        }
    }
}
