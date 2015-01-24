using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.ViewModels.Base;
using FactorioModBuilder.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Win32;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    /// <summary>
    /// The view model to represent a technology
    /// </summary>
    public class TechnologyVM : TreeItemVM<Technology, TechnologyVM>
    {
        /// <summary>
        /// The file path to this technology's icon image
        /// </summary>
        public string IconPath
        {
            get { return _internal.IconPath; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The number of research units that must be performed to research this technology
        /// </summary>
        public int UnitCount
        {
            get { return _internal.UnitCount; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The time, in seconds, that each unit takes to process
        /// </summary>
        public int UnitTime
        {
            get { return _internal.UnitTime; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The string used to determine display order for this technology
        /// </summary>
        public string Order
        {
            get { return _internal.Order; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The upgrade property of the recipe. Uncertain what this actually does
        /// </summary>
        public bool Upgrade
        {
            get { return _internal.Upgrade; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The list of ingredients required to process a single unit
        /// </summary>
        public ObservableCollection<TechnologyIngredientVM> UnitIngredients { get; private set; }

        /// <summary>
        /// The effects that are applied when this research is completed
        /// </summary>
        public ObservableCollection<TechnologyEffectVM> Effects { get; private set; }

        /// <summary>
        /// The technology prerequisites that must be researched in order for this technolgy
        /// to be available to the player
        /// </summary>
        public ObservableCollection<TechnologyPrerequisiteVM> Prerequisites { get; private set; }

        /// <summary>
        /// Command binding to add a new ingredient to the UnitIngredients collection
        /// </summary>
        public ICommand AddIngredientCmd { get { return this.GetCommand(this.AddIngredient); } }

        /// <summary>
        /// Command binding to remove all selected ingredients from the UnitIngredients collection
        /// </summary>
        public ICommand RemoveIngredientCmd { get { return this.GetCommand(this.RemoveIngredient, this.CanRemoveIngredient); } }

        /// <summary>
        /// Command binding to add a new effect to the Effects collection
        /// </summary>
        public ICommand AddEffectCmd { get { return this.GetCommand(this.AddEffect); } }

        /// <summary>
        /// Command binding to remvoe all selected effects from the Effects collection
        /// </summary>
        public ICommand RemoveEffectCmd { get { return this.GetCommand(this.RemoveEffect, this.CanRemoveEffect); } }

        /// <summary>
        /// Command binding to add a new prerequisite to the Prerequisites collection
        /// </summary>
        public ICommand AddPrereqCmd { get { return this.GetCommand(this.AddPrereq); } }

        /// <summary>
        /// Command binding to remove all seleccted prerequisites from the Prerequisistes collection
        /// </summary>
        public ICommand RemovePrereqCmd { get { return this.GetCommand(this.RemovePrereq, this.CanRemovePrereq); } }

        /// <summary>
        /// Command binding to display image selection dialog for the technology icon
        /// </summary>
        public ICommand FindImageCmd { get { return this.GetCommand(this.FindImage); } }

        public TechnologyVM(Technology tech)
            : this(null, tech)
        {
        }

        public TechnologyVM(TreeItemVMBase parent, Technology tech)
            : base(parent, tech)
        {
            this.UnitIngredients = new ObservableCollection<TechnologyIngredientVM>();
            this.Effects = new ObservableCollection<TechnologyEffectVM>();
            this.Prerequisites = new ObservableCollection<TechnologyPrerequisiteVM>();
        }

        /// <summary>
        /// Adds a new ingredient to the UnitIngredients collection
        /// </summary>
        private void AddIngredient()
        {
            this.UnitIngredients.Add(
                new TechnologyIngredientVM(
                    new TechnologyIngredient("", 1)));
        }

        /// <summary>
        /// Adds an new effect to the Effects collection
        /// </summary>
        private void AddEffect()
        {
            this.Effects.Add(
                new TechnologyEffectVM(
                    new TechnologyEffect()));
        }

        /// <summary>
        /// Adds a new prerequisite to the Prerequisites collection
        /// </summary>
        private void AddPrereq()
        {
            this.Prerequisites.Add(
                new TechnologyPrerequisiteVM(
                    new TechnologyPrerequisite()));
        }

        /// <summary>
        /// Determines whether any ingredients can be removed
        /// </summary>
        /// <returns>True if any ingredients are selected, otherwise false</returns>
        private bool CanRemoveIngredient()
        {
            return this.UnitIngredients.Any(o => o.IsSelected);
        }

        /// <summary>
        /// Determines whether any effects can be removed
        /// </summary>
        /// <returns>True if any effects are selected, otherwise false</returns>
        private bool CanRemoveEffect()
        {
            return this.Effects.Any(o => o.IsSelected);
        }

        /// <summary>
        /// Determines whether any prerequisites can be removed
        /// </summary>
        /// <returns>True if any prerequisites are selected, otherwise false</returns>
        private bool CanRemovePrereq()
        {
            return this.Prerequisites.Any(o => o.IsSelected);
        }

        /// <summary>
        /// Removes selected ingredients from the UnitIngredients collection
        /// </summary>
        private void RemoveIngredient()
        {
            this.UnitIngredients.RemoveWhere(o => o.IsSelected);
        }

        /// <summary>
        /// Removes selected effects from the Effects collection
        /// </summary>
        private void RemoveEffect()
        {
            this.Effects.RemoveWhere(o => o.IsSelected);
        }

        /// <summary>
        /// Removes selected prerequisites from the Prerequisites collection
        /// </summary>
        private void RemovePrereq()
        {
            this.Prerequisites.RemoveWhere(o => o.IsSelected);
        }

        /// <summary>
        /// Displays a dialog to select an appropriate image icon
        /// </summary>
        private void FindImage()
        {
            var ofd = new OpenFileDialog();
            ofd.CheckFileExists = true;
            ofd.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            ofd.Multiselect = false;

            if (ofd.ShowDialog() == true)
            {
                this.IconPath = ofd.FileName;
            }
        }
    }
}
