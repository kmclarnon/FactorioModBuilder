using FactorioModBuilder.Build;
using FactorioModBuilder.Build.Data;
using FactorioModBuilder.Build.Extensions;
using FactorioModBuilder.Models.ProjectItems;
using FactorioModBuilder.ViewModels.Base;
using FactorioModBuilder.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfUtils;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class ModInfoVM : ProjectItem<ModInfo, ModInfoVM>
    {
        public override IEnumerable<DataUnit> CompilerData
        {
            get
            {
                return new ModInfoData(this.ModName,
                    this.Version,
                    this.Title,
                    this.Author,
                    this.Contact,
                    this.Homepage,
                    this.Description).ListWrap()
                    .Concat(this.Dependencies.SelectMany(o => o.CompilerData));
            }
        }

        public ObservableCollection<ModInfoDependencyVM> Dependencies { get; private set; }

        public string ModName
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value, false, null, (x => this.UpdateProjectName())); }
        }

        public string Version
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value, false, null, (x => this.UpdateProjectVersion())); }

        }

        public string Title
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
        }

        public string Author
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
        }

        public string Contact
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
        }

        public string Homepage
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
        }

        public string Description
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
        }

        public ICommand RemoveDependencyCmd { get { return this.GetCommand(this.RemoveDependency, this.CanRemoveDependency); } }
        public ICommand AddDependencyCmd { get { return this.GetCommand(this.AddDependency, this.CanAddDependency); } }

        static ModInfoVM()
        {
            ModInfoVM.AddPropertyValidation("Version",
                (x => Regex.IsMatch(x.Version, @"^\d{1,4}\.\d{1,4}\.\d{1,4}$")),
                "Version must be in the form Major.Middle.Minor");
        }

        private int _depCount = 1;

        public ModInfoVM(ModInfo info)
            : base(info)
        {
            this.Dependencies = new ObservableCollection<ModInfoDependencyVM>();
        }

        public ModInfoVM(TreeItemVMBase parent, ModInfo info)
            : base(parent, info)
        {
        }

        private bool CanRemoveDependency()
        {
            return this.Dependencies.Where(o => o.IsSelected).Any();
        }

        private void RemoveDependency()
        {
            var lst = this.Dependencies.Where(o => o.IsSelected).ToList();
            foreach (var l in lst)
                this.Dependencies.Remove(l);
        }

        private bool CanAddDependency()
        {
            return true;
        }

        private void AddDependency()
        {
            this.Dependencies.Add(new ModInfoDependencyVM(
                new ModInfoDependency("New Dependency " + _depCount)));
            _depCount++;
        }

        private void UpdateProjectVersion()
        {
            ProjectVM res;
            if (this.TryFindElementUp<ProjectVM>(out res))
            {
                res.Version = this.Version;
            }
        }

        private void UpdateProjectName()
        {
            ProjectVM res;
            if (this.TryFindElementUp<ProjectVM>(out res))
            {
                res.Name = this.ModName;
            }
        }
    }
}
