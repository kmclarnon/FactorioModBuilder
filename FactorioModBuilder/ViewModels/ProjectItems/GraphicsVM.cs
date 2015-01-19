using FactorioModBuilder.Build.Data;
using FactorioModBuilder.Models.ProjectItems;
using FactorioModBuilder.ViewModels.Base;
using FactorioModBuilder.ViewModels.ProjectItems.Prototype;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class GraphicsVM : ProjectItem<Graphics, GraphicsVM>
    {
        public override IEnumerable<DataUnit> CompilerData
        {
            get { throw new NotImplementedException(); }
        }

        public ObservableCollection<GraphicsFilterVM> Filters { get; private set; }

        public ObservableCollection<IGraphicsSource> GraphicsSources
        {
            get
            {
                PrototypesVM res;
                if (!this.TryFindElementPeer(out res))
                    throw new Exception("Could not find prototypes view model");
                return res.GraphicsSources;
            }
        }

        public GraphicsVM(Graphics item)
            : this(null, item)
        {
        }

        public GraphicsVM(TreeItemVMBase parent, Graphics item)
            : base(parent, item)
        {
            this.Filters = new ObservableCollection<GraphicsFilterVM>();
            this.Filters.Add(
                new GraphicsFilterVM(this,
                    new GraphicsFilter("graphics"),
                    new List<GraphicsFilterVM>()
                    {
                        new GraphicsFilterVM(new GraphicsFilter("entity")),
                        new GraphicsFilterVM(new GraphicsFilter("equipment")),
                        new GraphicsFilterVM(new GraphicsFilter("icons")),
                        new GraphicsFilterVM(new GraphicsFilter("technology")),
                        new GraphicsFilterVM(new GraphicsFilter("terrain"))
                    }));
        }
    }
}
