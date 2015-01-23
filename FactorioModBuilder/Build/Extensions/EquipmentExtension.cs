using FactorioModBuilder.Build.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Extensions
{
    public class EquipmentExtension : ExtensionBase<EquipmentData>
    {
        public EquipmentExtension()
            : base(ExtensionType.PrototypeEquipment, ExtensionType.PrototypeItems)
        {
        }

        protected override bool BuildUnit(IEnumerable<EquipmentData> units, StringBuilder sb)
        {
            return true;
        }

        protected override bool ValidateData(IEnumerable<EquipmentData> units)
        {
            return true;
        }

        protected override bool GetOutputPath(out string path)
        {
            path = Path.Combine(this.PrototypeDirectory, "equipment.lua");
            return true;
        }
    }
}
