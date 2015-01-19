using FactorioModBuilder.Build;
using FactorioModBuilder.Build.Data;
using FactorioModBuilder.Build.Extensions;
using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using FactorioModBuilder.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class PrototypesVM : ProjectItem<Prototypes, PrototypesVM>
    {
        public override IEnumerable<DataUnit> CompilerData
        {
            get
            {
                GroupsVM gres;
                if (!this.TryFindElementDown(out gres))
                    throw new Exception("Could not find group child element");
                SubGroupsVM sgres;
                if (!this.TryFindElementDown(out sgres))
                    throw new Exception("Could not find subgroup child element");
                ItemsVM ires;
                if (!this.TryFindElementDown(out ires))
                    throw new Exception("Could not find items child element");
                RecipesVM rres;
                if (!this.TryFindElementDown(out rres))
                    throw new Exception("Could not find recipes child element");
                return gres.CompilerData.ConcatMany(sgres.CompilerData, ires.CompilerData, rres.CompilerData);
            }
        }

        public ObservableCollection<SubGroupVM> ItemSubgroups
        {
            get
            {
                SubGroupsVM res;
                if (!this.TryFindElementDown(out res))
                    throw new Exception("Could not find subgroups child element");
                return res.ItemList;
            }
        }

        public ObservableCollection<GroupVM> ItemGroups
        {
            get
            {
                GroupsVM res;
                if (!this.TryFindElementDown(out res))
                    throw new Exception("Could not find groups child element");
                return res.ItemList;
            }
        }

        public ObservableCollection<ItemVM> Items
        {
            get
            {
                ItemsVM res;
                if (!this.TryFindElementDown(out res))
                    throw new Exception("Could not find items child element");
                return res.ItemList;
            }
        }

        public ObservableCollection<IGraphicsSource> GraphicsSources { get; private set; }

        public PrototypesVM(Prototypes types, IEnumerable<TreeItemVMBase> children)
            : base(types, children)
        {
            this.GraphicsSources = new ObservableCollection<IGraphicsSource>();
        }

        public PrototypesVM(TreeItemVMBase parent, Prototypes types, IEnumerable<TreeItemVMBase> children)
            : base(parent, types, children)
        {
            this.GraphicsSources = new ObservableCollection<IGraphicsSource>();
        }

        protected override void Initialize()
        {
            this.Items.CollectionChanged += GraphicsSourceCollectionChanged;
            this.ItemGroups.CollectionChanged += GraphicsSourceCollectionChanged;
        }

        void GraphicsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var i in e.NewItems)
                        this.GraphicsSources.Add((IGraphicsSource)i);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var i in e.OldItems)
                        this.GraphicsSources.Remove((IGraphicsSource)i);
                    break;
                default:
                    throw new Exception("Unhandled graphics source change");
            }
        }
    }
}
