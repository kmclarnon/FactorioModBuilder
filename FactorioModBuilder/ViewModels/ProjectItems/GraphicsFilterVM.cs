using FactorioModBuilder.Models.ProjectItems;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class GraphicsFilterVM : ProjectItem<GraphicsFilter, GraphicsFilterVM>
    {
        public override IEnumerable<Build.Data.DataUnit> CompilerData
        {
            get { throw new NotImplementedException(); }
        }

        public ObservableCollection<GraphicsFilterItemVM> ItemList { get; private set; }

        public GraphicsFilterVM(GraphicsFilter item)
            : this(item, new List<GraphicsFilterVM>())
        {
        }

        public GraphicsFilterVM(GraphicsFilter item, IEnumerable<GraphicsFilterVM> children)
            : base(item, children)
        {
            this.ItemList = new ObservableCollection<GraphicsFilterItemVM>();
            this.ItemList.Add(new GraphicsFilterItemVM(new GraphicsFilterItem("test item")));
        }
    }
}
