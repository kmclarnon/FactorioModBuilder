using FactorioModBuilder.ViewModels.Main;
using FactorioModBuilder.ViewModels.ProjectItems;
using FactorioModBuilder.ViewModels.ProjectItems.Prototype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FactorioModBuilder
{
    public class ViewTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ModControlTemplate { get; set; }
        public DataTemplate ModInfoTemplate { get; set; }
        public DataTemplate ProjectHeaderTemplate { get; set; }
        public DataTemplate ItemGroupsTemplate { get; set; }
        public DataTemplate ItemSubGroupsTemplate { get; set; }
        public DataTemplate ItemsTemplate { get; set; }
        public DataTemplate PrototypeTemplate { get; set; }
        public DataTemplate EntitiesTemplate { get; set; }
        public DataTemplate EquipsTemplate { get; set; }
        public DataTemplate FluidsTemplate { get; set; }
        public DataTemplate RecipesTemplate { get; set; }
        public DataTemplate TechnologyTemplate { get; set; }
        public DataTemplate TilesTemplate { get; set; }
        public DataTemplate GraphicsTemplate { get; set; }

        private static Dictionary<Type, Func<ViewTemplateSelector, DataTemplate>> _typeDict = 
            new Dictionary<Type, Func<ViewTemplateSelector, DataTemplate>>()
        {
            { typeof(ModControlVM),     (x => x.ModControlTemplate) },
            { typeof(ModInfoVM),        (x => x.ModInfoTemplate) },
            { typeof(ProjectVM),        (x => x.ProjectHeaderTemplate) },
            { typeof(GroupsVM),         (x => x.ItemGroupsTemplate) },
            { typeof(SubGroupsVM),      (x => x.ItemSubGroupsTemplate) },
            { typeof(ItemsVM),          (x => x.ItemsTemplate) },
            { typeof(PrototypesVM),     (x => x.PrototypeTemplate) },
            { typeof(EntitiesVM),       (x => x.EntitiesTemplate) },
            { typeof(EquipsVM),         (x => x.EquipsTemplate) },
            { typeof(FluidsVM),         (x => x.FluidsTemplate) },
            { typeof(RecipesVM),        (x => x.RecipesTemplate) },
            { typeof(TechnologiesVM),   (x => x.TechnologyTemplate) },
            { typeof(TilesVM),          (x => x.TilesTemplate) },
            { typeof(GraphicsVM),       (x => x.GraphicsTemplate) }
        };

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if(item != null)
            {
                Func<ViewTemplateSelector, DataTemplate> func;
                if (item != null && _typeDict.TryGetValue(item.GetType(), out func))
                    return func(this);
            }

            // fall back on our base class
            return base.SelectTemplate(item, container);
        }
    }
}
