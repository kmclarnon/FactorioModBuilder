using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.Utility
{
    public abstract class TreeItem<T> : TreeItemBase
        where T : TreeItem<T>
    {
        public TreeItem(string name) : base(name)
        {
        }
    }
}
