using FactorioModBuilder.Build.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class PrototypeGroupsExtension : ExtensionBase
    {
        public PrototypeGroupsExtension()
            : base(ExtensionType.PrototypeGroups)
        {
        }

        public override bool BuildUnit(DataUnit unit, DirectoryInfo outDir)
        {
            var gd = unit as GroupsData;
            if(gd == null)
            {
                this.Error("Expected input to be groups data, received: {0}", unit.GetType().Name);
                return false;
            }

            using(var fs = File.Open(Path.Combine(outDir.FullName, "item-groups.lua"), FileMode.Create))
            using(var sw = new StreamWriter(fs))
            {

            }

            return true;
        }
    }
}
