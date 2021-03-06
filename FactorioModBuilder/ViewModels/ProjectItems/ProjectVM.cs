﻿using FactorioModBuilder.Models.ProjectItems;
using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.Models.Base;
using FactorioModBuilder.ViewModels.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using FactorioModBuilder.Extensions;
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
        public override IEnumerable<DataUnit> CompilerData
        {
            get
            {
                ModInfoVM miRes;
                if (!this.TryFindElementDown(out miRes))
                    throw new Exception("Failed to find mod info child");
                ModControlVM mcRes;
                if (!this.TryFindElementDown(out mcRes))
                    throw new Exception("Failed to find mod control child");
                PrototypesVM prot;
                if (!this.TryFindElementDown(out prot))
                    throw new Exception("Failed to find prototypes child");
                LocaleVM loc;
                if (!this.TryFindElementDown(out loc))
                    throw new Exception("Failed to find locale child");
                GraphicsVM gres;
                if (!this.TryFindElementDown(out gres))
                    throw new Exception("Failed to find graphics child");
                var test = gres.CompilerData;
                return new ProjectData(this.Name, this.TempDir, this.OutDir, this.Version).ListWrap()
                    .ConcatMany(miRes.CompilerData, mcRes.CompilerData, 
                        prot.CompilerData, loc.CompilerData, gres.CompilerData)
                    .Concat(new ModDataData().ListWrap());
            }
        }

        public string TempDir
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
        }

        public string OutDir
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
        }

        public string Version
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
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
