using FactorioModBuilder.Build.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Data
{
    [DataContract]
    public abstract class DataUnit
    {
        [IgnoreDataMember]
        public ExtensionType Type { get; private set; }
        [IgnoreDataMember]
        public List<DataUnit> SubUnits { get; private set; }

        public DataUnit(ExtensionType type)
        {
            this.Type = type;
            this.SubUnits = new List<DataUnit>();
        }
    }
}
