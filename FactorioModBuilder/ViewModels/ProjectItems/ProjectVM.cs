using FactorioModBuilder.Models.ProjectItems;
using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class ProjectVM : TreeItemVM<Project>
    {
        public ProjectVM(Project header)
            : base(null, header)
        {
            this.Children.Add(new ModInfoVM(this, new ModInfo()));
            this.Children.Add(new ModDataVM(this, new ModData()));
            this.Children.Add(new ModControlVM(this, new ModControl()));
            this.Children.Add(new PrototypesVM(this, new Prototypes()));
            this.Children.Add(new LocaleVM(this, new Locale()));
        }
    }
}
