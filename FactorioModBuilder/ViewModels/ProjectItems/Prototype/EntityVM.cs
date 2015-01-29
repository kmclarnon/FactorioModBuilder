using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    /// <summary>
    /// View model fro the Entity model
    /// </summary>
    public class EntityVM : ProjectItem<Entity, EntityVM>
    {
        /// <summary>
        /// The basic constructor to wrap an Entity model
        /// </summary>
        /// <param name="entity">The Entity model to wrap</param>
        public EntityVM(Entity entity)
            : base(entity, DoubleClickBehavior.OpenContent)
        {
        }

        /// <summary>
        /// THe basic constructor to wrap an Entity model and associte it with the provided parent
        /// </summary>
        /// <param name="parent">The parent of this view model</param>
        /// <param name="entity">The Entity model to wrap</param>
        public EntityVM(TreeItemVMBase parent, Entity entity)
            : base(parent, entity, DoubleClickBehavior.OpenContent)
        {
        }
    }
}
