using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using WpfUtils;

namespace FactorioModBuilder.ViewModels.Dialogs
{
    /// <summary>
    /// The view model backing the Color Dialog window
    /// </summary>
    public class ColorDialogVM : BaseVM
    {
        /// <summary>
        /// The old color set in the constructor
        /// </summary>
        public Brush OldColor
        {
            get { return this.GetProperty<Brush>(); }
            set { this.SetProperty(value, (() => this.NewColor = value)); }
        }

        /// <summary>
        /// The new color that the user is selecting
        /// </summary>
        public Brush NewColor
        {
            get { return this.GetProperty<Brush>(); }
            set { this.SetProperty(value); }
        }

        /// <summary>
        /// The current red value of the new color
        /// </summary>
        public byte RedValue
        {
            get { return this.GetProperty<byte>(); }
            set { this.SetProperty(value, this.OnSliderUpdate); }
        }

        /// <summary>
        /// The current green value of the new color
        /// </summary>
        public byte GreenValue
        {
            get { return this.GetProperty<byte>(); }
            set { this.SetProperty(value, this.OnSliderUpdate); }
        }

        /// <summary>
        /// The current blue value of the new color
        /// </summary>
        public byte BlueValue
        {
            get { return this.GetProperty<byte>(); }
            set { this.SetProperty(value, this.OnSliderUpdate); }
        }

        /// <summary>
        /// Closes the dialog and sets dialog result to true
        /// </summary>
        public ICommand OkCmd { get { return this.GetCommand(this.Ok, this.CanOk); } }

        /// <summary>
        /// Closes the dialog and sets dialog resutl to false
        /// </summary>
        public ICommand CancelCmd { get { return this.GetCommand(this.Cancel); } }

        private Action<bool> _setResult;

        public ColorDialogVM(Action<bool> setResult)
        {
            if (setResult == null)
                throw new ArgumentNullException("setResult");
            _setResult = setResult;
        }

        /// <summary>
        /// Checks whether or not the dialog can be closed successfully by the user
        /// </summary>
        /// <returns>True if the ok command can be executed</returns>
        private bool CanOk()
        {
            return true;
        }

        /// <summary>
        /// Closes the dialog and sets the dialog result to true
        /// </summary>
        private void Ok()
        {
            _setResult(true);
        }

        /// <summary>
        /// Closes the dialog and sets the dialog result to false
        /// </summary>
        private void Cancel()
        {
            _setResult(false);
        }

        /// <summary>
        /// Updates the color when the red, blue and green values change
        /// </summary>
        private void OnSliderUpdate()
        {
            if (this.NewColor == null)
                return;

            Color c = new Color();
            c.R = this.RedValue;
            c.G = this.GreenValue;
            c.B = this.BlueValue;
            this.NewColor = new SolidColorBrush(c);
        }
    }
}
