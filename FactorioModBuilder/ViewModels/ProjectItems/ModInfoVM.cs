using FactorioModBuilder.Build;
using FactorioModBuilder.Models.ProjectItems;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
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
                return new CompileUnit(
                    new Dictionary<string, CompileUnit>()
                {
                    { "\"name\"", new CompileUnit(this.ModName) },
                    { "\"version\"", new CompileUnit(this.Version) },
                    { "\"title\"", new CompileUnit(this.Title) },
                    { "\"author\"", new CompileUnit(this.Author) },
                    { "\"contact\"", new CompileUnit(this.Contact) },
                    { "\"homepage\"", new CompileUnit(this.Homepage) },
                    { "\"description\"", new CompileUnit(this.Description) }
                }, " : ");
            }
        }

        public override string CompilerKey
        {
            get { return "info.json"; }
        }

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

        public ICommand CancelCmd { get { return this.GetCommand(this.DoCancel, this.CanCancel); } }
        public ICommand ApplyCmd { get { return this.GetCommand(this.DoApply, this.CanApply); } }

        static ModInfoVM()
        {
            ModInfoVM.AddPropertyValidation("Version",
                (x => Regex.IsMatch(x.Version, @"^\d{1,4}.\d{1,4}.\d{1,4}$")),
                "Version must be in the form Major.Middle.Minor");
        }

        public ModInfoVM(ModInfo info)
            : base(info)
        {
        }

        public ModInfoVM(TreeItemVMBase parent, ModInfo info)
            : base(parent, info)
        {
        }

        private bool CanCancel()
        {
            return true;
        }

        private void DoCancel()
        {

        }

        private bool CanApply()
        {
            return true;
        }

        private void DoApply()
        {

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
