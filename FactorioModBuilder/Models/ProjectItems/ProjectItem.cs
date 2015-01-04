using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems
{
    public abstract class ProjectItem<T> : ProjectItemBase
        where T : ProjectItem<T>
    {
        public ProjectItem(string name) : base(name)
        {
        }
    }
}
