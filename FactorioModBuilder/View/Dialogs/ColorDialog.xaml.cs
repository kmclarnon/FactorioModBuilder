using FactorioModBuilder.ViewModels.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FactorioModBuilder.View.Dialogs
{
    /// <summary>
    /// Interaction logic for ColorDialog.xaml
    /// </summary>
    public partial class ColorDialog : Window
    {
        public Color OldColor 
        {
            set { ((ColorDialogVM)this.DataContext).OldColor = value; ; }
        }

        public Color SelectedColor
        {
            get { return ((ColorDialogVM)this.DataContext).NewColor; }
        }

        public ColorDialog()
        {
            InitializeComponent();
            this.DataContext = new ColorDialogVM(this.SetResult);
        }

        private void SetResult(bool res)
        {
            this.DialogResult = res;
            this.Close();
        }
    }


}
