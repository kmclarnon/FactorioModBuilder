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
using FactorioModBuilder.Build.Extensions;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class ProjectVM : ProjectItem<Project, ProjectVM>
    {
        public override CompileUnit CompilerData
        {
            get
            {
                CompileUnit res = new CompileUnit(ExtensionType.Project);
                res.Directives.Add(new OutputDirectory(this.OutDir));
                res.Directives.Add(new TempDirectory(this.TempDir));
                ModInfoVM miRes;
                if (!this.TryFindElementDown<ModInfoVM>(out miRes))
                    throw new Exception("Failed to find mod info child");
                res.Add(miRes.CompilerData);
                ModControlVM mcRes;
                if (!this.TryFindElementDown<ModControlVM>(out mcRes))
                    throw new Exception("Failed to find mod control child");
                res.Add(mcRes.CompilerData);
                ModDataVM mdRes;
                if (!this.TryFindElementDown<ModDataVM>(out mdRes))
                    throw new Exception("Failed to find mod data child");
                res.Add(mdRes.CompilerData);
                PrototypesVM prot;
                if (!this.TryFindElementDown<PrototypesVM>(out prot))
                    throw new Exception("Failed to find prototypes child");
                res.Add(prot.CompilerData);
                LocaleVM loc;
                if (!this.TryFindElementDown<LocaleVM>(out loc))
                    throw new Exception("Failed to find locale child");
                res.Add(loc.CompilerData);
                return res;
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
