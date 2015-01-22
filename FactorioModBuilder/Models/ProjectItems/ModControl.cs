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
    public class ModControl : TreeItem<ModControl>
    {
        public ModControl() : base("Mod Control")
        {

        }
    }
}
