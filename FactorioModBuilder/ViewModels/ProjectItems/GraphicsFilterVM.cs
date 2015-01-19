using FactorioModBuilder.Models.ProjectItems;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class GraphicsFilterVM : ProjectItem<GraphicsFilter, GraphicsFilterVM>
    {
        public override IEnumerable<Build.Data.DataUnit> CompilerData
        {
            get { throw new NotImplementedException(); }
        }

        public ObservableCollection<GraphicsFilterItemVM> ItemList { get; private set; }

        public ObservableCollection<IGraphicsSource> PossibleSources
        {
            get
            {
                GraphicsVM res;
                if (!this.TryFindElementUp(out res))
                    throw new Exception("Unable to locate parent graphics view model");
                return res.GraphicsSources;
            }
        }

        public ICommand AddGraphicCmd { get { return this.GetCommand(this.AddGraphic, this.CanAddGraphic); } }
        public ICommand RemoveGraphicCmd { get { return this.GetCommand(this.RemoveGraphic, this.CanRemoveGraphic); } }

        private int _newCount = 1;

        public GraphicsFilterVM(GraphicsFilter item)
            : base(item)
        {
            this.ItemList = new ObservableCollection<GraphicsFilterItemVM>();
        }

        public GraphicsFilterVM(TreeItemVMBase parent, GraphicsFilter item)
            : base(parent, item)
        {
            this.ItemList = new ObservableCollection<GraphicsFilterItemVM>();
        }

        public GraphicsFilterVM(GraphicsFilter item, IEnumerable<GraphicsFilterVM> children)
            : base(item, children)
        {
            this.ItemList = new ObservableCollection<GraphicsFilterItemVM>();
        }

        public GraphicsFilterVM(TreeItemVMBase parent, GraphicsFilter item, IEnumerable<GraphicsFilterVM> children)
            : base(parent, item, children)
        {
            this.ItemList = new ObservableCollection<GraphicsFilterItemVM>();
        }

        private bool CanAddGraphic()
        {
            return true;
        }

        private void AddGraphic()
        {
            this.ItemList.Add(new GraphicsFilterItemVM(
                new GraphicsFilterItem("New Graphic " + _newCount)));
            _newCount++;
        }

        private bool CanRemoveGraphic()
        {
            return this.ItemList.Where(o => o.IsSelected).Any();
        }

        private void RemoveGraphic()
        {
            var res = this.ItemList.Where(o => o.IsSelected).ToList();
            foreach (var r in res)
                this.ItemList.Remove(r);
        }
    }
}
