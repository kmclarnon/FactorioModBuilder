﻿using FactorioModBuilder.Models;
using FactorioModBuilder.Models.Dialogs;
using FactorioModBuilder.ViewModels;
using FactorioModBuilder.ViewModels.Dialogs;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace FactorioModBuilder.View.Dialogs
{
    /// <summary>
    /// Interaction logic for NewProject.xaml
    /// </summary>
    public partial class NewProjectDialog : MetroWindow
    {
        public NewProject NewProjectResult { get { return ((NewProjectVM)this.DataContext).Project; } }

        public NewProjectDialog()
        {
            InitializeComponent();
            this.DataContext = new NewProjectVM(this.SetResult);
        }

        private void SetResult(bool val)
        {
            this.DialogResult = val;
            this.Close();
        }

    }
}
