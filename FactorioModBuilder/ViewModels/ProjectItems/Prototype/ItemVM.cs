using FactorioModBuilder.Build.Data;
using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using FactorioModBuilder.Extensions;
using FactorioModBuilder.ViewModels.ProjectItems.Prototype;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfUtils;
using System.Text.RegularExpressions;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    /// <summary>
    /// View model for item prototypes
    /// </summary>
    public class ItemVM : ProjectItem<Item, ItemVM>, IGraphicsSource
    {
        /// <summary>
        /// Data to provide to the compiler at build time
        /// </summary>
        public override IEnumerable<DataUnit> CompilerData
        {
            get 
            { 
                return new ItemData(this.Name, this.IconPath, 
                    this.SubGroup, this.Order, 
                    this.PlaceResult, this.StackSize, this.Flag).ListWrap(); 
            }
        }

        /// <summary>
        /// Implementation of the IGraphicSource interface to allow automatic graphic management
        /// </summary>
        public string GraphicPath
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// Reference to the parent item subgroup to catch INotifyProperty changes of the Name
        /// </summary>
        public SubGroupVM SubGroupItem
        {
            get { return this.GetProperty<SubGroupVM>(); }
            set { this.SetProperty(value, false, this.UpdateSubGroupBinding, (x => this.SubGroup = x.Name)); }
        }

        /// <summary>
        /// Actual string name of the parent subgroup
        /// </summary>
        public string SubGroup
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// Order to dispaly item in its parent subgroup
        /// </summary>
        public string Order
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// File path to the icon that represents this item in the inventory
        /// </summary>
        public string IconPath
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value, false, null, (x => this.GraphicPath = (value == null) ? String.Empty : value)); }
        }

        /// <summary>
        /// Maximum stack size of the item in the players inventory
        /// </summary>
        public int StackSize
        {
            get { return this.GetProperty<int>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// Reference to the selected entity that should be spawned when the player places this item on the ground
        /// </summary>
        public EntityVM PlaceResultEntity
        {
            get { return this.GetProperty<EntityVM>(); }
            set { this.SetProperty(value, false, null, (x => this.PlaceResult = (value == null) ? String.Empty : value.Name)); }
        }

        /// <summary>
        /// Actual name of the referenced PlaceResultEntity
        /// </summary>
        public string PlaceResult
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// Relevant item flags
        /// </summary>
        public ItemFlag Flag
        {
            get { return this.GetProperty<ItemFlag>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// Command to bring up a dialog for the user to specify the icon image
        /// </summary>
        public ICommand FindImageCmd { get { return this.GetCommand(this.FindImage); } }

        /// <summary>
        /// The possible subgroups this item could belong to
        /// </summary>
        public ObservableCollection<SubGroupVM> SubGroups
        {
            get
            {
                PrototypesVM pvm;
                if (!this.TryFindElementUp(out pvm))
                    throw new Exception("Could not find prototypes parent");
                return pvm.SubGroups;
            }
        }

        /// <summary>
        /// The possible entities that could be placed by this item
        /// </summary>
        public ObservableCollection<EntityVM> PlaceResults
        {
            get
            {
                PrototypesVM pvm;
                if (!this.TryFindElementUp(out pvm))
                    throw new Exception("Could not find prototypes parent");
                return pvm.Entities;
            }
        }

        /// <summary>
        /// Command binding to allow the user to add a new recipe to the recipe list
        /// </summary>
        public ICommand AddRecipeCmd { get { return this.GetCommand(this.AddRecipe); } }

        /// <summary>
        /// Command binding to allow the user to remove a selected recipe from the list
        /// </summary>
        public ICommand RemoveRecipeCmd { get { return this.GetCommand(this.RemoveRecipes, this.CanRemoveRecipes); } }

        static ItemVM()
        {
            ItemVM.AddPropertyValidation("StackSize",
                (x => Regex.IsMatch(x.StackSize.ToString(), @"^\d+$")),
                "Stacksize must be a positive whole number");
        }

        /// <summary>
        /// View model wrapper for the Item model
        /// </summary>
        /// <param name="item">The Item model to wrap</param>
        public ItemVM(Item item)
            : this(null, item)
        {
        }

        /// <summary>
        /// View model wrapper for the Item model
        /// </summary>
        /// <param name="parent">The tree node parent of this view model</param>
        /// <param name="item">The Item model to wrap</param>
        public ItemVM(TreeItemVMBase parent, Item item)
            : base(parent, item, DoubleClickBehavior.OpenContent)
        {
        }

        /// <summary>
        /// Displays a file selection dialog that allows the user to select image files
        /// </summary>
        private void FindImage()
        {
            var ofd = new OpenFileDialog();
            ofd.CheckFileExists = true;
            ofd.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            ofd.Multiselect = false;

            if(ofd.ShowDialog() == true)
            {
                this.IconPath = ofd.FileName;
            }
        }

        /// <summary>
        /// Handles hooking and unhooking the SubGroupVM's property changed notification to catch renaming
        /// </summary>
        private void UpdateSubGroupBinding(SubGroupVM val)
        {
            if (this.SubGroupItem != null)
                this.SubGroupItem.PropertyChanged -= SubGroupPropertyChanged;
            if (val != null)
                val.PropertyChanged += SubGroupPropertyChanged;
        }

        /// <summary>
        /// Re-reads the name property from the subgroup whenever there is a property change
        /// </summary>
        private void SubGroupPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (this.SubGroupItem == null)
                this.SubGroup = String.Empty;
            else
                this.SubGroup = this.SubGroupItem.Name;
        }

        /// <summary>
        /// Adds a new recipe to the list
        /// </summary>
        private void AddRecipe()
        {
            this.Children.Add(new RecipeVM(new Recipe("New " + this.Name + "recipe")));
        }

        /// <summary>
        /// Determines if a recipe can be removed from the list
        /// </summary>
        /// <returns>True if any recipes are selected, otherwise false</returns>
        private bool CanRemoveRecipes()
        {
            return this.Children.Any(o => o.ContentIsSelected);
        }

        /// <summary>
        /// Removes all selected recipes from the list
        /// </summary>
        private void RemoveRecipes()
        {
            this.Children.RemoveWhere(o => o.ContentIsSelected);
        }
    }
}
