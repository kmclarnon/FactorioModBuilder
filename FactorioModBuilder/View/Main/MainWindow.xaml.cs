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
                        100,
                        new ModInfoExtension(),
                        new ModDependenciesExtension(),
                        new ProjectExtension(),
                        new ModDataExtension(),
                        new ModControlExtension(),
                        new PrototypesExtension(),
                        new PrototypeGroupsExtension(),
                        new PrototypeItemsExtension(),
                        new PrototypeEntitiesExtension(),
                        new PrototypeRecipeExtension(),
                        new LocaleExtension())));
        }
    }
}
