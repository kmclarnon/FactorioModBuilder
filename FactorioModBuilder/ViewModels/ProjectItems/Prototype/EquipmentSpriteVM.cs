using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    /// <summary>
    /// View model for the equipment sprite class
    /// </summary>
    public class EquipmentSpriteVM : ProjectItem<EquipmentSprite, EquipmentSpriteVM>
    {
        /// <summary>
        /// The filename of the sprite for this equipment
        /// </summary>
        public string FileName
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The width of the sprite specified by Filename
        /// </summary>
        public int Width
        {
            get { return this.GetProperty<int>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The height of the sprite specified by Filename
        /// </summary>
        public int Height
        {
            get { return this.GetProperty<int>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The priority of the sprite listed by Filename
        /// </summary>
        public SpritePriority Priority
        {
            get { return this.GetProperty<SpritePriority>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// Command binding to allow the user to select a sprite image file
        /// </summary>
        public ICommand SelectSpriteCmd { get { return this.GetCommand(this.SelectSprite); } }

        public EquipmentSpriteVM(EquipmentSprite item)
            : base(item, DoubleClickBehavior.OpenParent)
        {
        }

        public EquipmentSpriteVM(TreeItemVMBase parent, EquipmentSprite item)
            : base(parent, item, DoubleClickBehavior.OpenParent)
        {
        }

        /// <summary>
        /// Displays dialog for user sprite file selection
        /// </summary>
        private void SelectSprite()
        {
            var ofd = new OpenFileDialog();
            ofd.CheckFileExists = true;
            ofd.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            ofd.Multiselect = false;

            if (ofd.ShowDialog() == true)
            {
                this.FileName = ofd.FileName;
            }
        }
    }
}
