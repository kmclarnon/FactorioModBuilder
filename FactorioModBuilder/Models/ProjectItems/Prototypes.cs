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
            this.Children.Add(new ItemGroups());
        }
    }
}
