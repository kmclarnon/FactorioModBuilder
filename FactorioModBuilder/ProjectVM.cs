﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfUtils;

namespace FactorioModBuilder
{
    public class ProjectVM : BaseVM
    {
        public ObservableCollection<ProjectItemVM> ProjectItems { get; private set; }

        public ProjectVM()
        {
            this.ProjectItems = new ObservableCollection<ProjectItemVM>();
        }
    }
}
