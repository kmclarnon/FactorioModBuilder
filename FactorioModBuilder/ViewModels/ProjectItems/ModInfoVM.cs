using FactorioModBuilder.Build;
using FactorioModBuilder.Build.Extensions;
using FactorioModBuilder.Models.ProjectItems;
using FactorioModBuilder.ViewModels.Base;
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
        public override CompileUnit CompilerData
        {
            get
            {
                return new CompileUnit()
                {
                    { "Name", new CompileUnit(this.ModName) },
                    { "Version", new CompileUnit(this.Version) },
                    { "Title", new CompileUnit(this.Title) },
                    { "Author", new CompileUnit(this.Author) },
                    { "Contact", new CompileUnit(this.Contact) },
                    { "Homepage", new CompileUnit(this.Homepage) }, 
                    { "Description", new CompileUnit(this.Description) },
                    { "Dependencies", new CompileUnit(this.Dependencies.Select(o => o.CompilerData), ExtensionType.FactorioDependencies) }
                };
            }
        }

        public ObservableCollection<ModInfoDependencyVM> Dependencies { get; private set; }

        public string ModName
        {
            get { return _internal.ModName; }
            set { this.SetProperty(_internal, value, false, this.UpdateProjectName); }
        }

        public string Version
        {
            get { return _internal.Version; }
            set { this.SetProperty(_internal, value, false, this.UpdateProjectName); }

        }

        public string Title
        {
            get { return _internal.Title; }
            set { this.SetProperty(_internal, value); }
        }

        public string Author
        {
            get { return _internal.Author; }
            set { this.SetProperty(_internal, value); }
        }

        public string Contact
        {
            get { return _internal.Contact; }
            set { this.SetProperty(_internal, value); }
        }

        public string Homepage
        {
            get { return _internal.Homepage; }
            set { this.SetProperty(_internal, value); }
        }

        public string Description
        {
            get { return _internal.Description; }
            set { this.SetProperty(_internal, value); }
        }

        public ICommand RemoveDependencyCmd { get { return this.GetCommand(this.RemoveDependency, this.CanRemoveDependency); } }
        public ICommand AddDependencyCmd { get { return this.GetCommand(this.AddDependency, this.CanAddDependency); } }

        static ModInfoVM()
        {
            ModInfoVM.AddPropertyValidation("Version",
                (x => Regex.IsMatch(x.Version, @"^\d{1,4}.\d{1,4}.\d{1,4}$")),
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

        private void UpdateProjectName()
        {
            ProjectVM res;
            if (this.TryFindElementUp<ProjectVM>(out res))
            {
                if (this.Version == null || this.Version == String.Empty)
                    res.Name = this.ModName;
                else
                    res.Name = this.ModName + "_" + this.Version;
            }
        }
    }
}
