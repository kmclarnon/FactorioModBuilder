using FactorioModBuilder.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems.Prototype
{
    public class Item : TreeItem<Item>
    {
        public string SubGroup { get; set; }
        public string Order { get; set; }
        public string IconPath { get; set; }
        public int StackSize { get; set; }
        public string PlaceResult { get; set; }

        [Flags]
        public enum ItemFlag
        {
            GoesToQuickbar      = 1 << 0,
            GoesToMainInventory = 1 << 1,
            Hidden              = 1 << 2
        };

        public ItemFlag Flag { get; set; }

        public Item(string name) : base(name)
        {
        }
    }
}
