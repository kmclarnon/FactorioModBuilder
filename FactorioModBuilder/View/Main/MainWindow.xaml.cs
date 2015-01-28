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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FactorioModBuilder.ViewModels;
using FactorioModBuilder.Models;
using FactorioModBuilder.Models.Main;
using FactorioModBuilder.Build;
using FactorioModBuilder.Build.Extensions;

namespace FactorioModBuilder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainVM ViewModel { get { return (MainVM)this.DataContext; } }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainVM(
                new MainModel(
                    1200, 800,
                    "Factorio Mod Builder",
                    System.Reflection.Assembly.GetExecutingAssembly()
                        .GetName().Version.ToString(),
                    new Build.Compiler(
                        100, true,
                        new EntitiesExtension(),
                        new EquipmentExtension(),
                        new FluidsExtension(),
                        new GraphicsExtension(),
                        new GroupsExtension(),
                        new ItemsExtension(),
                        new LocaleExtension(),
                        new ModControlExtension(),
                        new ModDataExtension(),
                        new ModDependenciesExtension(),
                        new ModInfoExtension(),
                        new ProjectExtension(),
                        new PrototypesExtension(),
                        new RecipeExtension(),
                        new TechnologyExtension(),
                        new TilesExtension())));
        }

        private void OnActivated(object sender, EventArgs e)
        {
            this.ViewModel.Active = true;
        }

        private void OnDeactivated(object sender, EventArgs e)
        {
            this.ViewModel.Active = false;
        }

        /// <summary>
        /// Handles the title bar mouse down event. Drag if single click, resize if double click
        /// </summary>
        private void TitleBarMouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                if (e.ClickCount == 2)
                    this.ToggleWindowState();
                else
                    Application.Current.MainWindow.DragMove();
            }
        }

        private void MaxButton_Click(object sender, RoutedEventArgs e)
        {
            this.ToggleWindowState();
        }

        private void MinButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void ToggleWindowState()
        {
            if(this.WindowState == System.Windows.WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
