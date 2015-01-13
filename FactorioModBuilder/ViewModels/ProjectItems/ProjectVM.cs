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
using FactorioModBuilder.Build.Extensions;
using FactorioModBuilder.Build.Data;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class ProjectVM : ProjectItem<Project, ProjectVM>
    {
        public override DataUnit CompilerData
        {
            get
            {
                ModInfoVM miRes;
                if (!this.TryFindElementDown<ModInfoVM>(out miRes))
                    throw new Exception("Failed to find mod info child");
                ModControlVM mcRes;
                if (!this.TryFindElementDown<ModControlVM>(out mcRes))
                    throw new Exception("Failed to find mod control child");
                ModDataVM mdRes;
                if (!this.TryFindElementDown<ModDataVM>(out mdRes))
                    throw new Exception("Failed to find mod data child");
                PrototypesVM prot;
                if (!this.TryFindElementDown<PrototypesVM>(out prot))
                    throw new Exception("Failed to find prototypes child");
                LocaleVM loc;
                if (!this.TryFindElementDown<LocaleVM>(out loc))
                    throw new Exception("Failed to find locale child");
                return new ProjectData(this.Name, this.TempDir, this.OutDir,
                    (ModInfoData)miRes.CompilerData, (ModControlData)mcRes.CompilerData,
                    (ModDataData)mdRes.CompilerData, (PrototypesData)prot.CompilerData,
                    (LocaleData)loc.CompilerData);
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
