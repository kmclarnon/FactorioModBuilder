using FactorioModBuilder.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems.Prototype
{
    public class Technology : TreeItem<Technology>
    {
        public Technology(string name) : base(name)
        {
        }
    }
}
