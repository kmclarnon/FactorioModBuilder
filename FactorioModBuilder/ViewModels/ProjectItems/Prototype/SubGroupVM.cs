using FactorioModBuilder.Build;
using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class SubGroupVM : ProjectItem<SubGroup, SubGroupVM>
    {
        public override CompileUnit CompilerData
        {
            get
            {
                Dictionary<string, CompileUnit> res = new Dictionary<string, CompileUnit>();
                res.Add("type", new CompileUnit(this.Type));
                res.Add("name", new CompileUnit(this.Name));
                res.Add("group", new CompileUnit(this.Group));
                res.Add("order", new CompileUnit(this.Order));
                return new CompileUnit(res);
            }
        }

        public override string CompilerKey
        {
            get { return "prototypes.subgroups"; }
        }

        public string Type
        {
            get { return _internal.Type; }
            set { this.SetProperty(_internal, value); }
        }

        public string Group
        {
            get { return _internal.Group; }
            set { this.SetProperty(_internal, value); }
        }

        public string Order
        {
            get { return _internal.Order; }
            set { this.SetProperty(_internal, value); }
        }

        private bool _enabled;
        public bool Enabled
        {
            get { return _enabled; }
            set { this.SetProperty(ref _enabled, value); }
        }

        public SubGroupVM(SubGroup item)
            : this(null, item)
        {
        }

        public SubGroupVM(TreeItemVMBase parent, SubGroup item)
            : base(parent, item)
        {
            this.Enabled = true;
        }
    }
}
