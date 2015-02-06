using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    public class EquipmentShapeVM : ProjectItem<EquipmentShape, EquipmentShapeVM>
    {
        /// <summary>
        /// Width of the equipment in the armor gui
        /// </summary>
        public int Width
        {
            get { return this.GetProperty<int>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// Height of the equipment in the armor gui
        /// </summary>
        public int Height
        {
            get { return this.GetProperty<int>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// Type of the equipment shape
        /// </summary>
        public ShapeType Type
        {
            get { return this.GetProperty<ShapeType>(); }
            set { this.SetProperty(value); }
        }

        public EquipmentShapeVM(EquipmentShape item)
            : base(item, DoubleClickBehavior.OpenParent)
        {
        }

        public EquipmentShapeVM(TreeItemVMBase parent, EquipmentShape item)
            : base(parent, item, DoubleClickBehavior.OpenParent)
        {
        }
    }
}
