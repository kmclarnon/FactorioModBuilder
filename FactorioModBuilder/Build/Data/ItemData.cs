using FactorioModBuilder.Models.ProjectItems.Prototype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Build.Data
{
    public class ItemData : DataUnit
    {
        public string Name { get; private set; }
        public string Icon { get; private set; }
        public string SubGroup { get; private set; }
        public string Order { get; private set; }
        public string PlaceResult { get; private set; }
        public int StackSize { get; private set; }
        public ItemFlag Flag { get; private set; }

        public ItemData(string name, string icon, string subgroup,
            string order, string placeResult, int stackSize, ItemFlag flag)
            : base(Extensions.ExtensionType.PrototypeItems)
        {
            this.Name = name;
            this.Icon = icon;
            this.SubGroup = subgroup;
            this.Order = order;
            this.PlaceResult = placeResult;
            this.StackSize = StackSize;
            this.Flag = flag;
        }
    }
}
