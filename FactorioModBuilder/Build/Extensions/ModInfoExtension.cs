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
    public class ModInfoExtension : ExtensionBase<ModInfoData>
    {
        public ModInfoExtension() : base(ExtensionType.FactorioInfo) { }

        protected override bool BuildUnit(IEnumerable<ModInfoData> units)
        {
            string res;
            if (!this.Build(units, out res))
                return false;
            try
            {
                using (var fs = File.Open(Path.Combine(this.TemporaryDirectory, "info.json"), FileMode.Create))
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

        private bool Build(IEnumerable<ModInfoData> units, out string value)
        {
            // check that the modinfo data is valid
            var mi = units.Single();

            // verify our version number is the correct format
            if(!Regex.IsMatch(mi.Version, @"^\d{1,4}\.\d{1,4}\.\d{1,4}$"))
            {
                value = null;
                this.Error("Version format incorrect.  Version must be in Major.Mid.Minor format");
                return false;
            }

            MemoryStream ms = new MemoryStream();

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ModInfoData));
            ser.WriteObject(ms, mi);

            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            value = sr.ReadToEnd();

            return true;
        }
    }
}
