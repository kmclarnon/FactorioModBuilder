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

namespace FactorioModBuilder.ViewModels.ProjectItems.Prototype
{
    /// <summary>
    /// View model to wrap a fluid prototype used in the Fluids Screen
    /// </summary>
    public class FluidVM : ProjectItem<Fluid, FluidVM>
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
                    this.PressureToSpeed, this.FlowToEnergy, this.Order).ListWrap();
            }
        }

        /// <summary>
        /// The default temperature of the fluid
        /// </summary>
        public int DefaultTemp
        {
            get { return _internal.DefaultTemp; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The numeric value of the heat capacity
        /// </summary>
        public int HeatCapacity
        {
            get { return _internal.HeatCapacity; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The unit metric joules prefix associated with heat capacity
        /// </summary>
        public EnergyUnit HeatCapacityUnit
        {
            get { return _internal.HeatCapacityUnit; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The color of the fluid when it is not moving
        /// </summary>
        public Color BaseColor
        {
            get { return _internal.BaseColor; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The color of the fluid when it is moving through pipes
        /// </summary>
        public Color FlowColor
        {
            get { return _internal.FlowColor; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The maximum temperature that the fluid can be heated too
        /// </summary>
        public int MaxTemp
        {
            get { return _internal.MaxTemp; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The file path to the image to be used as an icon for this prototype
        /// </summary>
        public string IconPath
        {
            get { return _internal.IconPath; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The fluid's pressure to speed ratio
        /// </summary>
        public float PressureToSpeed
        {
            get { return _internal.PressureToSpeed; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The fluids speed to energy ratio
        /// </summary>
        public float FlowToEnergy
        {
            get { return _internal.FlowToEnergy; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The order to display this fluid in
        /// </summary>
        public string Order
        {
            get { return _internal.Order; }
            set { this.SetProperty(_internal, value); }
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
        public ICommand SelectIconCmd { get { return this.GetCommand(this.SelectIcon); } }

        public FluidVM(Fluid fluid)
            : this(null, fluid)
        {
        }

        public FluidVM(TreeItemVMBase parent, Fluid fluid)
            : base(parent, fluid)
        {
            this.BaseColor = Colors.White;
            this.FlowColor = Colors.White;
        }

        /// <summary>
        /// Provides a dialog allowing the user to pick the fluid base color
        /// </summary>
        private void SelectBaseColor()
        {
            var dlg = new ColorDialog();
            dlg.OldColor = this.BaseColor;
            dlg.Owner = Application.Current.MainWindow;
           
            if(dlg.ShowDialog() == true)
                this.BaseColor = dlg.SelectedColor;
        }

        /// <summary>
        /// Provides a dialog allowing the user to pick the fluid flow color
        /// </summary>
        private void SelectFlowColor()
        {
            var dlg = new ColorDialog();
            dlg.OldColor = this.FlowColor;
            dlg.Owner = Application.Current.MainWindow;

            if (dlg.ShowDialog() == true)
                this.FlowColor = dlg.SelectedColor;
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
    }
}
