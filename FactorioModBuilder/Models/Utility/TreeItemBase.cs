using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.Utility
{
    public abstract class TreeItemBase
    {
        public string Name { get; set; }

        public TreeItemBase(string name)
        {
            this.Name = name;
        }
    }
}
