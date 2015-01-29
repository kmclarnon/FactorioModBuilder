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
using System.Windows.Interop;
using System.Runtime.InteropServices;

namespace FactorioModBuilder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainVM ViewModel { get { return (MainVM)this.DataContext; } }

        private const int WM_SYSCOMMAND = 0x112;
        private HwndSource _hwndSource;

        public MainWindow()
        {
            this.SourceInitialized += MainWindow_SourceInitialized;
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

        private void MainWindow_SourceInitialized(object sender, EventArgs e)
        {
            _hwndSource = PresentationSource.FromVisual((Visual)sender) as HwndSource;
            if (_hwndSource == null)
                throw new Exception("Could not retrieve window handle");
            _hwndSource.AddHook(new HwndSourceHook(this.WndProc));
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case 0x0024:/* WM_GETMINMAXINFO */
                    WmGetMinMaxInfo(hwnd, lParam);
                    handled = true;
                    break;
            }

            return IntPtr.Zero;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public POINT(System.Drawing.Point pt) : this(pt.X, pt.Y) { }

            public static implicit operator System.Drawing.Point(POINT p)
            {
                return new System.Drawing.Point(p.X, p.Y);
            }

            public static implicit operator POINT(System.Drawing.Point p)
            {
                return new POINT(p.X, p.Y);
            }
        }

        [DllImport("user32.dll", SetLastError=true)]
        private static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);

        [DllImport("user32.dll")]
        private static extern IntPtr MonitorFromWindow(IntPtr hwnd, int dwFlags);

        [DllImport("kernel32.dll")]
        private static extern uint GetLastError();

        [StructLayout(LayoutKind.Sequential)]
        private struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MONITORINFO
        {
            public int cbSize;
            public RECT rcMonitor;
            public RECT rcWork;
            public uint dwFlags;
        }

        private enum MonitorOptions : int
        {
            MONITOR_DEFAULTTONULL = 0x00000000,
            MONITOR_DEFAULTTOPRIMARY = 0x00000001,
            MONITOR_DEFAULTTONEAREST = 0x00000002
        }

        private static void WmGetMinMaxInfo(System.IntPtr hwnd, System.IntPtr lParam)
        {
            MINMAXINFO mmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));

            // Adjust the maximized size and position to fit the work area of the correct monitor
            System.IntPtr monitor = MonitorFromWindow(hwnd, (int)MonitorOptions.MONITOR_DEFAULTTONEAREST);

            if (monitor != System.IntPtr.Zero)
            {

                MONITORINFO monitorInfo = new MONITORINFO();
                monitorInfo.cbSize = Marshal.SizeOf(monitorInfo);
                if (!GetMonitorInfo(monitor, ref monitorInfo))
                {
                    int val = Marshal.GetLastWin32Error();
                    return;
                }
                RECT rcWorkArea = monitorInfo.rcWork;
                RECT rcMonitorArea = monitorInfo.rcMonitor;
                mmi.ptMaxPosition.X = Math.Abs(rcWorkArea.Left - rcMonitorArea.Left);
                mmi.ptMaxPosition.Y = Math.Abs(rcWorkArea.Top - rcMonitorArea.Top);
                mmi.ptMaxSize.X = Math.Abs(rcWorkArea.Right - rcWorkArea.Left);
                mmi.ptMaxSize.Y = Math.Abs(rcWorkArea.Bottom - rcWorkArea.Top);
            }

            Marshal.StructureToPtr(mmi, lParam, true);
        }

        #region Resize Handling

        /// <summary>
        /// Direction enum to simplify the send message function calls
        /// </summary>
        private enum ResizeDirection
        {
            Left = 1,
            Right = 2,
            Top = 3,
            TopLeft = 4,
            TopRight = 5,
            Bottom = 6,
            BottomLeft = 7,
            BottomRight = 8,
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        private void ResizeWindow(ResizeDirection dir)
        {
            SendMessage(_hwndSource.Handle, WM_SYSCOMMAND, (IntPtr)(61440 + dir), IntPtr.Zero);
        }

        private void TopLeftMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.ResizeWindow(ResizeDirection.TopLeft);
        }

        private void TopRightMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.ResizeWindow(ResizeDirection.TopRight);
        }

        private void BottomLeftMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.ResizeWindow(ResizeDirection.BottomLeft);
        }

        private void BottomRightMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.ResizeWindow(ResizeDirection.BottomRight);
        }

        private void TopMiddleMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.ResizeWindow(ResizeDirection.Top);
        }

        private void BottomMiddleMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.ResizeWindow(ResizeDirection.Bottom);
        }

        private void LeftMiddleMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.ResizeWindow(ResizeDirection.Left);
        }

        private void RightMiddleMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.ResizeWindow(ResizeDirection.Right);
        }

        #endregion /* Resize Handling */
    }
}
