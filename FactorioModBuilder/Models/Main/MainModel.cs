using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioModBuilder.Models.Main
{
    public class MainModel
    {
        public string AppTitle { get { return this.AppName + " v" + this.AppVersion; } }
        public string AppName { get { return "Factorio Mod Builder"; } }
        public string AppVersion { get { return "0.0.1"; } }
        public int AppHeight { get; set; }
        public int AppWidth { get; set; }

        public MainModel(int width, int height)
        {
            this.AppHeight = height;
            this.AppWidth = width;
        }
    }
}
