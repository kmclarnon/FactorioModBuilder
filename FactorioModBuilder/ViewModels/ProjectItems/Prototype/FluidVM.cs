using FactorioModBuilder.Models.ProjectItems.Prototype;
using FactorioModBuilder.View.Dialogs;
using FactorioModBuilder.ViewModels.Base;
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
    public class FluidVM : TreeItemVM<Fluid, FluidVM>
    {
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
        public Fluid.EnergyUnit HeatCapacityUnit
        {
            get { return _internal.HeatCapacityUnit; }
            set { this.SetProperty(_internal, value); }
        }

        /// <summary>
        /// The color of the fluid when it is not moving
        /// </summary>
        public SolidColorBrush BaseColor
        {
            get { return this.GetProperty<SolidColorBrush>(); }
            set { this.SetProperty(value, (() => this.UpdateBaseColor(value))); }
        }

        /// <summary>
        /// The color of the fluid when it is moving through pipes
        /// </summary>
        public SolidColorBrush FlowColor
        {
            get { return this.GetProperty<SolidColorBrush>(); }
            set { this.SetProperty(_internal, value, false, (() => this.UpdateFlowColor(value))); }
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
            : base(fluid)
        {
        }

        public FluidVM(TreeItemVMBase parent, Fluid fluid)
            : base(parent, fluid)
        {
        }

        private void UpdateBaseColor(SolidColorBrush b)
        {
            _internal.BaseColorR = b.Color.R;
            _internal.BaseColorG = b.Color.G;
            _internal.BaseColorB = b.Color.B;
        }

        private void UpdateFlowColor(SolidColorBrush b)
        {
            _internal.FlowColorR = b.Color.R;
            _internal.FlowColorG = b.Color.G;
            _internal.FlowColorB = b.Color.B;
        }

        private void SelectBaseColor()
        {
            var dlg = new ColorDialog();
            dlg.OldColor = Colors.Red;
            dlg.Owner = Application.Current.MainWindow;
           

            if(dlg.ShowDialog() == true)
            {
                this.BaseColor = new SolidColorBrush(dlg.NewColor);
            }
        }

        private void SelectFlowColor()
        {

        }

        private void SelectIcon()
        {

        }
    }
}
