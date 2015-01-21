using FactorioModBuilder.Build.Data;
using FactorioModBuilder.Models.ProjectItems;
using FactorioModBuilder.ViewModels.Base;
using FactorioModBuilder.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FactorioModBuilder.ViewModels.ProjectItems.Prototype;

namespace FactorioModBuilder.ViewModels.ProjectItems
{
    public class GraphicsFilterVM : ProjectItem<GraphicsFilter, GraphicsFilterVM>
    {
        public override IEnumerable<Build.Data.DataUnit> CompilerData
        {
            get 
            {
                return this.ItemList.Select(o =>
                    new GraphicsData(o.Source.Name, o.ImportPath, o.ExportPath))
                    .Concat(this.Children.Where(o => o.GetType() == typeof(GraphicsFilterVM))
                        .Cast<GraphicsFilterVM>().SelectMany(e => e.CompilerData));
            }
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

        public string FilterPath
        {
            get
            {
                GraphicsFilterVM filterRes;
                if(!this.TryFindElementUp(out filterRes))
                {
                    GraphicsVM res;
                    if (!this.TryFindElementUp(out res))
                        throw new Exception("Could not find parent graphics view model");
                    return this.Name;
                }

                return filterRes.FilterPath + "/" + this.Name;
            }
        }

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
            this.ItemList.Add(new GraphicsFilterItemVM(this,
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

        public void UpdateExportPath()
        {
            foreach(var c in this.Children)
            {
                var filter = c as GraphicsFilterVM;
                if (filter != null)
                    filter.UpdateExportPath();
            }

            foreach (var i in this.ItemList)
                i.Update();
        }

        public void AddGraphicsSource(string fPath, IGraphicsSource source)
        {
            if(fPath == this.FilterPath)
            {
                var res = new GraphicsFilterItemVM(this, new GraphicsFilterItem(source.Name));
                res.Source = source;
                this.ItemList.Add(res);
            }
            else if(fPath.StartsWith(this.FilterPath))
            {
                var cs = this.Children.Cast<GraphicsFilterVM>().Where(o => fPath.StartsWith(o.FilterPath));
                if (!cs.Any())
                {
                    // determine what the filter's name should be
                    var rem = fPath.Substring(this.FilterPath.Length + 1);
                    string name;
                    if (rem.Contains('/'))
                        name = rem.Substring(0, rem.IndexOf('/'));
                    else
                        name = rem;

                    var newFilter = new GraphicsFilterVM(this, new GraphicsFilter(name));
                    this.Children.Add(newFilter);
                    newFilter.AddGraphicsSource(fPath, source);
                }
                else
                    cs.Single().AddGraphicsSource(fPath, source);
            }
        }

        public void RemoveGraphicsSource(IGraphicsSource source)
        {
            var res = this.ItemList.Where(o => o.Source.Equals(source)).ToList();
            foreach (var r in res)
                this.ItemList.Remove(r);
            foreach (var c in this.Children.Cast<GraphicsFilterVM>())
                c.RemoveGraphicsSource(source);
        }
    }
}
