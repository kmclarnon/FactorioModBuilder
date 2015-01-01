using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems
{
    public class GameItem : ProjectItem
    {
        public string Type { get { return "item"; } }
        public string Subgroup { get; set; }
        public string Order { get; set; }
        public bool Enabled { get; set; }
        public string IconPath { get; set; }
        public int StackSize { get; set; }

        public GameItem(string name) : base(name)
        {
        }
    }
}
