using FactorioModBuilder.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems.Prototype
{
    public enum ItemFlag
    {
        [DescriptionAttribute("Goes to Quickbar")]
        GoesToQuickbar,
        [DescriptionAttribute("Goes to Main Inventory")]
        GoesToMainInventory,
        [DescriptionAttribute("Goes to Quickbar, Hidden")]
        GoesToQuickBarHidden,
        [DescriptionAttribute("Goes to Main Inventory, Hidden")]
        GoesToMainInventoryHidden,
    };

    public class Item : TreeItem<Item>
    {
        public string SubGroup { get; set; }
        public string Order { get; set; }
        public string IconPath { get; set; }
        public int StackSize { get; set; }
        public string PlaceResult { get; set; }

        public ItemFlag Flag { get; set; }

        public Item(string name) : base(name)
        {
        }
    }
}
