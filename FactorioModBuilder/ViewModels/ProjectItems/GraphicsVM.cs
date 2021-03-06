﻿using FactorioModBuilder.Build.Data;
using FactorioModBuilder.Models.ProjectItems;
using FactorioModBuilder.ViewModels.Base;
using FactorioModBuilder.ViewModels.ProjectItems.Prototype;
using FactorioModBuilder.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class GraphicsVM : ProjectItem<Graphics, GraphicsVM>
    {
        public override IEnumerable<DataUnit> CompilerData
        {
            get { return this.Filters.SelectMany(o => o.CompilerData); }
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

        public bool ManualMode
        {
            get { return this.GetProperty<bool>(); }
            set { this.SetProperty(value); }
        }

        public GraphicsVM(Graphics item)
            : this(null, item)
        {
        }

        public GraphicsVM(TreeItemVMBase parent, Graphics item)
            : base(parent, item, DoubleClickBehavior.OpenContent)
        {
            this.Filters = new ObservableCollection<GraphicsFilterVM>();
            this.Filters.Add(new GraphicsFilterVM(this,
                    new GraphicsFilter("graphics")));
        }

        protected override void Initialize()
        {
            this.GraphicsSources.CollectionChanged += GraphicsSources_CollectionChanged;
        }

        void GraphicsSources_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var filter = this.Filters.Single();
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (!this.ManualMode)
                    {
                        foreach (var i in e.NewItems)
                            filter.AddGraphicsSource(this.Categorize((IGraphicsSource)i), (IGraphicsSource)i);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var i in e.OldItems)
                        filter.RemoveGraphicsSource((IGraphicsSource)i);
                    break;
                default:
                    throw new Exception("Unhandled collection changed event");
            }
        }

        private void UpdateChildPaths()
        {
            foreach (var f in this.Filters)
                f.UpdateExportPath();
        }

        private static Dictionary<Type, string> _categoryDict = new Dictionary<Type, string>()
        {
            { typeof(FluidVM), "graphics/fluids" },
            { typeof(GroupVM), "graphics/groups" },
            { typeof(ItemVM), "graphics/items" },
            { typeof(TechnologyVM), "graphics/technology" },
            { typeof(TileVM), "graphics/tiles" }
        };

        private string Categorize(object source)
        {
            return _categoryDict[source.GetType()];
        }
    }
}
