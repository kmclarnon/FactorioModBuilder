using FactorioModBuilder.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FactorioModBuilder
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Dictionary<string, Action<MainVM, IEnumerable<string>>> _commands =
            new Dictionary<string, Action<MainVM, IEnumerable<string>>>()
        {
            // appearance
            { "width", App.SetWidth },
            { "height", App.SetHeight },
            { "top", App.SetTop },
            { "left", App.SetLeft },
            // startup
            { "create", App.Create }
        };

        private string _cmdSelector;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow wnd = new MainWindow();
            if (e.Args.Length > 0)
            {
                List<string> parameters = new List<string>();
                Action<MainVM, IEnumerable<string>> cmdFunc;
                foreach(var arg in e.Args)
                {
                    // check if we have a command
                    if (arg.StartsWith("-"))
                    {
                        // new command, dispatch previous one
                        if (_cmdSelector != null && _commands.TryGetValue(_cmdSelector, out cmdFunc))
                            cmdFunc(wnd.ViewModel, parameters);
                        // clear our parameter buffer for next command
                        parameters.Clear();
                        // start processing new command
                        _cmdSelector = arg.Substring(1);
                    }
                    // otherwise save as parameter
                    else
                        parameters.Add(arg);
                }

                // process the last pending command
                if (_commands.TryGetValue(_cmdSelector, out cmdFunc))
                    cmdFunc(wnd.ViewModel, parameters);
            }

            wnd.Show();
        }

        private static void SetWidth(MainVM vm, IEnumerable<string> args)
        {
            var largs = args.ToList();
            if (largs.Count != 1)
                return;

            int res;
            if (Int32.TryParse(largs[0], out res))
                vm.AppWidth = res;
        }

        private static void SetHeight(MainVM vm, IEnumerable<string> args)
        {
            var largs = args.ToList();
            if (largs.Count != 1)
                return;

            int res;
            if (Int32.TryParse(largs[0], out res))
                vm.AppHeight = res;
        }

        private static void SetTop(MainVM vm, IEnumerable<string> args)
        {
            var largs = args.ToList();
            if (largs.Count != 1)
                return;

            int res;
            if (Int32.TryParse(largs[0], out res))
                vm.AppTop = res;
        }

        private static void SetLeft(MainVM vm, IEnumerable<string> args)
        {
            var largs = args.ToList();
            if (largs.Count != 1)
                return;

            int res;
            if (Int32.TryParse(largs[0], out res))
                vm.AppLeft = res;
        }

        private static void Create(MainVM vm, IEnumerable<string> args)
        {
            var largs = args.ToList();
            if (largs.Count != 3)
                return;

            FileInfo finfo = null;
            try
            {
                finfo = new FileInfo(largs[2]);
            }
            catch (Exception) { }
            if (ReferenceEquals(finfo, null))
                return;
            else
            {
                if(largs[0] == null || largs[0] == String.Empty || 
                   largs[1] == null || largs[1] == String.Empty)
                    return;
                vm.CreateAndLoadNewSolution(largs[0], largs[1], largs[2]);
            }
        }
    }
}
