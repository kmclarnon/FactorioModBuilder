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
            if (_parent.Solutions.Count < 1)
                throw new InvalidOperationException("Cannot create solution data when no solution exists");

            List<DataUnit> res = new List<DataUnit>();
            var sln = _parent.Solutions[0];
            foreach (ProjectVM p in sln.Children)
                _parent.Compiler.Build(p.CompilerData.ToList());
        }

        private bool CanBuildSolution()
        {
            return _parent.Solutions.Count > 0;
        }

        private void RebuildSolution()
        {

        }

        private bool CanRebuildSolution()
        {
            return _parent.Solutions.Count > 0;
        }

        private void CleanSolution()
        {

        }

        private bool CanCleanSolution()
        {
            return _parent.Solutions.Count > 0;
        }
    }
}
