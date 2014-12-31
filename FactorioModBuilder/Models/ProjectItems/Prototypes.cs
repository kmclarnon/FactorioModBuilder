using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems
{
    public class Prototypes : ProjectItem
    {
        public Prototypes()
            : base("Prototypes")
        {
            this.Children.Add(new Groups());
            this.Children.Add(new SubGroups());
            this.Children.Add(new GameItems());
        }
    }
}
