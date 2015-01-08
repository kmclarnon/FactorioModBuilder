using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfUtils;

namespace FactorioModBuilder.ViewModels.Main
{
    public class BuildMenuVM
    {
        private ICommand _buildSolutionCmd;
        public ICommand BuildSolutionCmd
        {
            get
            {
                if (_buildSolutionCmd == null)
                    _buildSolutionCmd = new RelayCommand(
                        (x => this.BuildSolution()), (x => this.CanBuildSolution()));
                return _buildSolutionCmd;
            }
        }

        private ICommand _rebuildSolutionCmd;
        public ICommand RebuildSolutionCmd
        {
            get
            {
                if (_rebuildSolutionCmd == null)
                    _rebuildSolutionCmd = new RelayCommand(
                        (x => this.RebuildSolution()), (x => this.CanRebuildSolution()));
                return _rebuildSolutionCmd;
            }
        }

        private ICommand _cleanSolutionCmd;
        public ICommand CleanSolutionCmd
        {
            get
            {
                if (_cleanSolutionCmd == null)
                    _cleanSolutionCmd = new RelayCommand(
                        (x => this.CleanSolution()), (x => this.CanCleanSolution()));
                return _cleanSolutionCmd;
            }
        }

        private MainVM _parent;

        public BuildMenuVM(MainVM parent)
        {
            _parent = parent;
        }

        private void BuildSolution()
        {
            var data = _parent.GetSolutionData();
            _parent.Compiler.Build("test", "tmp", data);
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
