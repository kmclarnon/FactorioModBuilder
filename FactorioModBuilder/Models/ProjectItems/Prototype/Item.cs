using FactorioModBuilder.Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems.Prototype
{
    public class Item : TreeItem<Item>
    {
        public string Type { get { return "item"; } }
        public string Subgroup { get; set; }
        public string Order { get; set; }
        public bool Enabled { get; set; }
        public string IconPath { get; set; }
        public int StackSize { get; set; }

        public Item(string name) : base(name)
        {
        }
    }
}
