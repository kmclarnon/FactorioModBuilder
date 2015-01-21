using FactorioModBuilder.Build.Data;
using FactorioModBuilder.Models.ProjectItems;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class GraphicsFilterItemVM : ProjectItem<GraphicsFilterItem, GraphicsFilterItemVM>
    {
        public override IEnumerable<DataUnit> CompilerData
        {
            get { throw new NotImplementedException(); }
        }

        public string ExportPath
        {
            get { return _internal.ExportPath; }
            set { this.SetProperty(_internal, value); }
        }

        public string ImportPath
        {
            get { return _internal.ImportPath; }
            set { this.SetProperty(_internal, value); }
        }

        public string ParentPath
        {
            get
            {
                GraphicsFilterVM res;
                if (!this.TryFindElementUp(out res))
                    throw new Exception("Could not find parent graphics filter view model");
                return res.FilterPath;
            }
        }

        public IGraphicsSource Source
        {
            get { return this.GetProperty<IGraphicsSource>(); }
            set { this.SetProperty(value, this.SourceUpdated); }
        }

        public GraphicsFilterItemVM(GraphicsFilterItem item)
            : this(null, item)
        {
        }

        public GraphicsFilterItemVM(TreeItemVMBase parent, GraphicsFilterItem item)
            : base(parent, item)
        {
        }

        private void SourceUpdated()
        {
            this.Source.PropertyChanged += OnSourcePropertyChanged;
            this.ImportPath = this.Source.GraphicPath;
            this.UpdateExportPath();
        }

        void OnSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.ImportPath = this.Source.GraphicPath;
            this.UpdateExportPath();
        }

        public void UpdateExportPath()
        {
            if(this.Source != null)
                this.ExportPath = this.ParentPath + "/" +
                    Path.GetFileName(this.Source.GraphicPath);
        }
    }
}
