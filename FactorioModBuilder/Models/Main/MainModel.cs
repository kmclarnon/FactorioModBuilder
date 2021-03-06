﻿using FactorioModBuilder.Build;
using FactorioModBuilder.Build.Extensions;
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
        public string AppName { get; private set; }
        public string AppVersion { get; private set; }
        public int AppHeight { get; set; }
        public int AppWidth { get; set; }
        public int AppTop { get; set; }
        public int AppLeft { get; set; }

        public Compiler Compiler { get; private set; }

        public MainModel(int width, int height, 
            string name, string version, Compiler c)
        {
            this.AppHeight = height;
            this.AppWidth = width;
            this.AppName = name;
            this.AppVersion = version;
            this.Compiler = c;
        }

        
    }
}
