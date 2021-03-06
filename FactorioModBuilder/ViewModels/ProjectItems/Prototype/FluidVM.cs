﻿using FactorioModBuilder.Build.Data;
using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.View.Dialogs;
using FactorioModBuilder.ViewModels.Base;
using FactorioModBuilder.Extensions;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using WpfUtils.Extensions;
using System.Collections.ObjectModel;

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    /// <summary>
    /// View model to wrap a fluid prototype used in the Fluids Screen
    /// </summary>
    public class FluidVM : ProjectItem<Fluid, FluidVM>, IGraphicsSource
    {
        /// <summary>
        /// The data necessary for the compiler
        /// </summary>
        public override IEnumerable<DataUnit> CompilerData
        {
            get
            {
                return new FluidData(this.Name, this.HeatCapacity, this.HeatCapacityUnit,
                    this.BaseColor, this.FlowColor, this.DefaultTemp, this.MaxTemp,
                    this.PressureToSpeed, this.FlowToEnergy, this.Order, 
                    this.IconPath, this.SubGroup).ListWrap();
            }
        }

        /// <summary>
        /// Exposes the path to the file associated with this fluid's icon
        /// </summary>
        public string GraphicPath
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The default temperature of the fluid
        /// </summary>
        public int DefaultTemp
        {
            get { return this.GetProperty<int>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The numeric value of the heat capacity
        /// </summary>
        public int HeatCapacity
        {
            get { return this.GetProperty<int>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The unit metric joules prefix associated with heat capacity
        /// </summary>
        public EnergyUnit HeatCapacityUnit
        {
            get { return this.GetProperty<EnergyUnit>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The color of the fluid when it is not moving
        /// </summary>
        public Color BaseColor
        {
            get { return this.GetProperty<Color>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The color of the fluid when it is moving through pipes
        /// </summary>
        public Color FlowColor
        {
            get { return this.GetProperty<Color>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The maximum temperature that the fluid can be heated too
        /// </summary>
        public int MaxTemp
        {
            get { return this.GetProperty<int>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The file path to the image to be used as an icon for this prototype
        /// </summary>
        public string IconPath
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value, false, null, (x => this.GraphicPath = (value == null) ? String.Empty : value)); }
        }

        /// <summary>
        /// The subgroup view model binding to facility renaming
        /// </summary>
        public SubGroupVM SubGroupItem
        {
            get { return this.GetProperty<SubGroupVM>(); }
            set { this.SetProperty(value, false, this.UpdateSubGroupBinding, (x => this.SubGroup = x.Name)); }
        }

        /// <summary>
        /// The subgroup property of the fluid
        /// </summary>
        public string SubGroup
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The fluid's pressure to speed ratio
        /// </summary>
        public float PressureToSpeed
        {
            get { return this.GetProperty<float>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The fluids speed to energy ratio
        /// </summary>
        public float FlowToEnergy
        {
            get { return this.GetProperty<float>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The order to display this fluid in
        /// </summary>
        public string Order
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// Provides a color selection dialog to select the base color
        /// </summary>
        public ICommand SelectBaseColorCmd { get { return this.GetCommand(this.SelectBaseColor); } }

        /// <summary>
        /// Provides a color selection dialog to select the flow color
        /// </summary>
        public ICommand SelectFlowColorCmd { get { return this.GetCommand(this.SelectFlowColor); } }

        /// <summary>
        /// Provides a file selection dialog to select the fluid icon
        /// </summary>
        public ICommand FindImageCmd { get { return this.GetCommand(this.SelectIcon); } }

        /// <summary>
        /// The available subgroups to select as a subgroup for this fluid
        /// </summary>
        public ObservableCollection<SubGroupVM> SubGroups
        {
            get
            {
                PrototypesVM pvm;
                if (!this.TryFindElementUp(out pvm))
                    throw new Exception("Could not find parent prototypes view model");
                return pvm.SubGroups;
            }
        }

        public FluidVM(Fluid fluid)
            : this(null, fluid)
        {
        }

        public FluidVM(TreeItemVMBase parent, Fluid fluid)
            : base(parent, fluid, DoubleClickBehavior.OpenContent)
        {
            this.BaseColor = Colors.White;
            this.FlowColor = Colors.White;
        }

        /// <summary>
        /// Provides a dialog allowing the user to pick the fluid base color
        /// </summary>
        private void SelectBaseColor()
        {

        }

        /// <summary>
        /// Provides a dialog allowing the user to pick the fluid flow color
        /// </summary>
        private void SelectFlowColor()
        {

        }

        /// <summary>
        /// Provides a dialog for the user to select an appropriate image file
        /// </summary>
        private void SelectIcon()
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

        /// <summary>
        /// Handles hooking and unhooking the SubGroupVM's property changed notification to catch renaming
        /// </summary>
        /// <param name="val"></param>
        private void UpdateSubGroupBinding(SubGroupVM val)
        {
            if(this.SubGroupItem != null)
                this.SubGroupItem.PropertyChanged -= SubGroupPropertyChanged;
            if (val != null)
                val.PropertyChanged += SubGroupPropertyChanged;
        }

        /// <summary>
        /// Re-reads the name property from the subgroup whenever there is a property change
        /// </summary>
        void SubGroupPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (this.SubGroupItem == null)
                this.SubGroup = String.Empty;
            else
                this.SubGroup = this.SubGroupItem.Name;
        }

        /// <summary>
        /// Used to force the Subgroup value to null when the specified subgroup
        /// was deleted by the user
        /// </summary>
        public void ForceRemoveSubGroup()
        {
            this.SetProperty(null, (() => this.SubGroup));
            this.SubGroup = String.Empty;
        }
    }
}
