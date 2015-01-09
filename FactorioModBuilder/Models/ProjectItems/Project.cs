using FactorioModBuilder.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.ProjectItems
{
    public class Project : TreeItem<Project>
    {
        public string TempDir { get; set; }
        public string OutDir { get; set; }

        public Project(string name, string tmpdir, 
            string outdir) : base(name)
        {
            this.TempDir = tmpdir;
            this.OutDir = outdir;
        }
    }
}
