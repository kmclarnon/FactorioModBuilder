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
        public Color OldColor
        {
            get { return this.GetProperty<Color>(); }
            set { this.SetProperty(value, null, this.OnOldColorUpdate); }
        }

        /// <summary>
        /// The new color that the user is selecting
        /// </summary>
        private Color _newColor;
        public Color NewColor
        {
            get { return _newColor; }
            set { this.SetProperty(ref _newColor, value); }
        }

        public byte Red
        {
            get { return this.GetProperty<byte>(); }
            set { this.SetProperty(value, null, this.OnSliderUpdate); }
        }

        public byte Green
        {
            get { return this.GetProperty<byte>(); }
            set { this.SetProperty(value, null, this.OnSliderUpdate); }
        }

        public byte Blue
        {
            get { return this.GetProperty<byte>(); }
            set { this.SetProperty(value, null, this.OnSliderUpdate); }
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
            this.OldColor = Colors.White;
            this.NewColor = Colors.White;
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

        private void OnSliderUpdate()
        {
            _newColor.R = this.Red;
            _newColor.G = this.Green;
            _newColor.B = this.Blue;
            this.NotifyPropertyChanged(() => this.NewColor);
        }

        private void OnOldColorUpdate()
        {
            this.NewColor = this.OldColor;
            this.Red = this.NewColor.R;
            this.Blue = this.NewColor.B;
            this.Green = this.NewColor.G;
        }
    }
}
