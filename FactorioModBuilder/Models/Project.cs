using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FactorioModBuilder.Models.ProjectItems;

namespace FactorioModBuilder
{
    public class Project
    {
        public ProjectHeader ProjectItem { get; private set; }

        public Project()
        {
            this.ProjectItem = new ProjectHeader("Realism Mod Project");
            this.ProjectItem.Children.Add(new ModInfo("RealismMod", "0.0.1",
                "Realism Total Overhaul Mod", "DopplerEffect", "DopplerEffect@gmail.com",
                "http://singularity.tk/realism", "The Realism Mod completely overhauls the base game"));
            this.ProjectItem.Children.Add(new ModData());
            this.ProjectItem.Children.Add(new ModControl());
            this.ProjectItem.Children.Add(new Prototypes());
        }
    }
}
