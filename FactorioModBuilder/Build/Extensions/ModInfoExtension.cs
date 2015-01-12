using FactorioModBuilder.Build.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class ModInfoExtension : ExtensionBase
    {
        public override ExtensionType Extension
        {
            get { return ExtensionType.FactorioInfo; }
        }

        public override bool BuildUnit(DataUnit unit, System.IO.DirectoryInfo outDir)
        {
            throw new NotImplementedException();
        }

        public override bool BuildUnit(DataUnit unit, out string value)
        {
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ModInfoData));
            ser.WriteObject(ms, unit);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            value = sr.ReadToEnd();
            return true;
        }
    }
}
