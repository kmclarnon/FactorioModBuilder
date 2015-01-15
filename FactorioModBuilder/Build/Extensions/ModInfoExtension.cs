using FactorioModBuilder.Build.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class ModInfoExtension : ExtensionBase
    {
        public ModInfoExtension() : base(ExtensionType.FactorioInfo) { }

        public override bool BuildUnit(DataUnit unit, DirectoryInfo outDir)
        {
            string res;
            if (!this.BuildUnit(unit, out res))
                return false;
            try
            {
                using (var fs = File.Open(Path.Combine(outDir.FullName, "info.json"), FileMode.Create))
                using (var sw = new StreamWriter(fs))
                {
                    sw.Write(res);
                }
            }
            catch(Exception e)
            {
                this.Fatal("Encountered exception creating info.json: {0}", e.Message);
                return false;
            }

            return true;
        }

        public override bool BuildUnit(DataUnit unit, out string value)
        {
            // check that the modinfo data is valid
            var mi = unit as ModInfoData;
            if(mi == null)
            {
                value = null;
                this.Error("Expected to recieve mod info data, recieved: {0}", unit.GetType().FullName);
                return false;
            }

            // verify our version number is the correct format
            if(!Regex.IsMatch(mi.Version, @"^\d{1,4}\.\d{1,4}\.\d{1,4}$"))
            {
                value = null;
                this.Error("Version format incorrect.  Version must be in Major.Mid.Minor format");
                return false;
            }

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
