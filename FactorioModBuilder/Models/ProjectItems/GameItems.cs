using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems
{
    public class GameItems : ProjectItem
    {
        public List<GameItem> ItemList { get; private set; }

        public GameItems() : base("Items")
        {
            this.ItemList = new List<GameItem>();

            ItemList.Add(new GameItem());
            ItemList.Add(new GameItem());
        }
    }
}
