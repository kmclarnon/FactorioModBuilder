using Microsoft.Win32;
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
using WpfUtils;

namespace FactorioModBuilder.View
{
    /// <summary>
    /// Interaction logic for NewProject.xaml
    /// </summary>
    public partial class NewProject : Window
    {
        public enum SolutionType
        {
            CreateNew,
            AddExisting,
            CreateInNewInstance
        }

        private ICommand _okCmd;
        public ICommand OkCmd
        {
            get
            {
                if (_okCmd == null)
                    _okCmd = new RelayCommand(
                        (x => this.OK()));
                return _okCmd;
            }
        }

        private ICommand _cancelCmd;
        public ICommand CancelCmd
        {
            get
            {
                if (_cancelCmd == null)
                    _cancelCmd = new RelayCommand(
                        (x => this.Cancel()));
                return _cancelCmd;
            }
        }

        private ICommand _browseLocation;
        public ICommand BrowseLocationCmd
        {
            get
            {
                if (_browseLocation == null)
                    _browseLocation = new RelayCommand(
                        (x => this.BrowseLocation()));
                return _browseLocation;
            }
        }

        public string ResultProjectName { get; private set; }
        public string ResultLocation { get; private set; }
        public SolutionType ResultSolutionType { get; private set; }
        public string ResultSolutionName { get; private set; }
        public IEnumerable<Tuple<SolutionType, String>> PossibleSolutions
        {
            get 
            {
                return new List<Tuple<SolutionType, string>>()
                {
                    new Tuple<SolutionType, string>(
                        SolutionType.CreateNew, "Create New"),
                    new Tuple<SolutionType, string>(
                        SolutionType.AddExisting, "Add Existing"),
                    new Tuple<SolutionType, string>(
                        SolutionType.CreateInNewInstance, "Create In New Instance")
                }; 
            }
        }

        public NewProject()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void OK()
        {
            this.DialogResult = true;
            this.Close();
        }

        private void Cancel()
        {
            this.DialogResult = false;
            this.Close();
        }

        private void BrowseLocation()
        {
           
        }
    }
}
