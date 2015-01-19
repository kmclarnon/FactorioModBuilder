using FactorioModBuilder.Build.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class PrototypesExtension : ExtensionBase<PrototypeData>
    {
        public PrototypesExtension()
            : base(ExtensionType.Prototypes, ExtensionType.FactorioGraphics)
        {
        }

        protected override bool BuildUnit(IEnumerable<PrototypeData> units, StreamWriter sw)
        {
            this.PrototypeDirectory = Path.Combine(this.TemporaryDirectory, "prototypes");
            var dinfo = new DirectoryInfo(this.PrototypeDirectory);
            
            if (!dinfo.Exists)
                dinfo.Create();
            else
            {
                foreach (var file in dinfo.GetFiles())
                    file.Delete();
                foreach (var folder in dinfo.GetDirectories())
                    folder.Delete(true);
            }

            return true;
        }
    }
}
