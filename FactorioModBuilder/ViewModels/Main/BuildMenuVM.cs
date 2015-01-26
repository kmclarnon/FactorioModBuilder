using FactorioModBuilder.Build;
using FactorioModBuilder.Build.Data;
using FactorioModBuilder.ViewModels.ProjectItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfUtils;

namespace FactorioModBuilder.ViewModels.Main
{
    public class BuildMenuVM : BaseVM
    {
        public ICommand BuildSolutionCmd { get { return this.GetCommand(this.BuildSolution, this.CanBuildSolution); } }
        public ICommand RebuildSolutionCmd { get { return this.GetCommand(this.RebuildSolution, this.CanRebuildSolution); } }
        public ICommand CleanSolutionCmd { get { return this.GetCommand(this.CleanSolution, this.CanCleanSolution); } }

        private MainVM _parent;

        public BuildMenuVM(MainVM parent)
        {
            _parent = parent;
        }

        private void BuildSolution()
        {

        }

        private bool CanBuildSolution()
        {
            return false;
        }

        private void RebuildSolution()
        {

        }

        private bool CanRebuildSolution()
        {
            return false;
        }

        private void CleanSolution()
        {

        }

        private bool CanCleanSolution()
        {
            return false;
        }
    }
}
