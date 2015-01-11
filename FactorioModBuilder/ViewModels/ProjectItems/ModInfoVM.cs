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
            get { return _mItem.ModName; }
            set { this.SetProperty(_mItem, value, false, this.UpdateProjectName); }
        }

        public string Version
        {
            get { return _mItem.Version; }
            set { this.SetProperty(_mItem, value, false, this.UpdateProjectName); }

        }

        public string Title
        {
            get { return _mItem.Title; }
            set { this.SetProperty(_mItem, value); }
        }

        public string Author
        {
            get { return _mItem.Author; }
            set { this.SetProperty(_mItem, value); }
        }

        public string Contact
        {
            get { return _mItem.Contact; }
            set { this.SetProperty(_mItem, value); }
        }

        public string Homepage
        {
            get { return _mItem.Homepage; }
            set { this.SetProperty(_mItem, value); }
        }

        public string Description
        {
            get { return _mItem.Description; }
            set { this.SetProperty(_mItem, value); }
        }

        private ICommand _cancelCmd;
        public ICommand CancelCmd
        {
            get
            {
                if (_cancelCmd == null)
                {
                    _cancelCmd = new RelayCommand((x => this.DoCancel()),
                        (x => this.CanCancel()));
                }
                return _cancelCmd;
            }
        }

        private ICommand _applyCmd;
        public ICommand ApplyCmd
        {
            get
            {
                if(_applyCmd == null)
                {
                    _applyCmd = new RelayCommand((x => this.DoApply()),
                        (x => this.CanApply()));
                }
                return _applyCmd;
            }
        }

        private ModInfo _mItem { get { return (ModInfo)_item; } }

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
