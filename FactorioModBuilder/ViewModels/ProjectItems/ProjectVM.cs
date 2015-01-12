using FactorioModBuilder.Models.ProjectItems;
using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.Models.Base;
using FactorioModBuilder.ViewModels.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FactorioModBuilder.Build;
using FactorioModBuilder.Build.Directives;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class ProjectVM : ProjectItem<Project, ProjectVM>
    {
        public override CompileUnit CompilerData
        {
            get
            {
                Dictionary<string, CompileUnit> res = new Dictionary<string, CompileUnit>();
                foreach (var c in this.SubUnits)
                    res.Add(c.CompilerKey, c.CompilerData);

                var cu = new CompileUnit(res);
                cu.Directives.Add(new OutputDirectory(this.OutDir));
                cu.Directives.Add(new TempDirectory(this.TempDir));
                cu.Directives.Add(new ProjectName(this.Name));
                return cu;
            }
        }

        public string TempDir
        {
            get { return _internal.TempDir; }
            set
            {
                if(_internal.TempDir != value)
                {
                    _internal.TempDir = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public string OutDir
        {
            get { return _internal.OutDir; }
            set
            {
                if(_internal.OutDir != value)
                {
                    _internal.OutDir = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public ProjectVM(Project header, IEnumerable<TreeItemVMBase> projectChildren)
            : base(header, projectChildren)
        {
        }

        public ProjectVM(TreeItemVMBase parent, Project header, IEnumerable<TreeItemVMBase> projectChildren)
            : base(parent, header, projectChildren)
        {
        }
    }
}
