﻿using FactorioModBuilder.ViewModels.ProjectItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfUtils;
using FactorioModBuilder.Models;
using System.Windows.Input;
using FactorioModBuilder.View;
using FactorioModBuilder.Models.SolutionItems;

namespace FactorioModBuilder.ViewModels
{
    public class MainVM : BaseVM
    {
        public string AppTitle { get { return _main.AppTitle; } }
        public string AppName { get { return _main.AppName; } }
        public string AppVersion { get { return _main.AppVersion; } }
        
        public int AppHeight
        {
            get { return _main.AppHeight; }
            set
            {
                if(_main.AppHeight != value)
                {
                    _main.AppHeight = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public int AppWidth
        {
            get { return _main.AppWidth; }
            set
            {
                if(_main.AppWidth != value)
                {
                    _main.AppWidth = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        private SolutionVM _activeSolution;
        public SolutionVM ActiveSolution
        {
            get { return _activeSolution; }
            set
            {
                if(_activeSolution != value)
                {
                    _activeSolution = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        private ICommand _newProjectCmd;
        public ICommand NewProjectCmd
        {
            get
            {
                if (_newProjectCmd == null)
                    _newProjectCmd = new RelayCommand(
                        (x => this.NewProject()), (x => this.CanNewProject()));
                return _newProjectCmd;
            }
        }

        private ICommand _openProjectCmd;
        public ICommand OpenProjectCmd
        {
            get
            {
                if (_openProjectCmd == null)
                    _openProjectCmd = new RelayCommand(
                        (x => this.OpenProject()), (x => this.CanOpenProject()));
                return _openProjectCmd;
            }
        }

        private ICommand _saveProjectCmd;
        public ICommand SaveProjectCmd
        {
            get
            {
                if (_saveProjectCmd == null)
                    _saveProjectCmd = new RelayCommand(
                        (x => this.SaveProject()), (x => this.CanSaveProject()));
                return _saveProjectCmd;
            }
        }

        private ICommand _saveProjectAsCmd;
        public ICommand SaveProjectAsCmd
        {
            get
            {
                if (_saveProjectCmd == null)
                    _saveProjectCmd = new RelayCommand(
                        (x => this.SaveProjectAs()), (x => this.CanSaveProjectAs()));
                return _saveProjectAsCmd;
            }
        }

        private ICommand _closeProjectCmd;
        public ICommand CloseProjectCmd
        {
            get
            {
                if (_closeProjectCmd == null)
                    _closeProjectCmd = new RelayCommand(
                        (x => this.CloseProject()), (x => this.CanCloseProject()));
                return _closeProjectCmd;
            }
        }

        private ICommand _exitCmd;
        public ICommand ExitCmd
        {
            get
            {
                if (_exitCmd == null)
                    _exitCmd = new RelayCommand(
                        (x => this.Exit()), (x => this.CanExit()));
                return _exitCmd;
            }
        }

        private Main _main;

        public MainVM(Main m)
        {
            _main = m;
            this.ActiveSolution = new SolutionVM(
                new Solution("Test Solution",
                    new Project("Test Project")));
        }

        private bool CanNewProject()
        {
            return true;
        }

        private void NewProject()
        {
            var npw = new NewProjectDialog();
            npw.Owner = App.Current.MainWindow;
            npw.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            
            if(npw.ShowDialog() == true)
            {
                var result = npw.NewProjectResult;
                switch (result.ResultSolutionType)
                {
                    case SolutionType.CreateNew:
                        this.ActiveSolution = new SolutionVM(
                            new Solution(result.ResultSolutionName, 
                                new Project(result.ResultProjectName)));
                        break;
                    case SolutionType.AddExisting:
                        break;
                    case SolutionType.CreateInNewInstance:
                        break;
                    default:
                        throw new ArgumentException("Unknown Solution Type");
                }
            }
        }

        private bool CanOpenProject()
        {
            return true;
        }

        private void OpenProject()
        {
        }

        private bool CanSaveProject()
        {
            return true;
        }

        private void SaveProject()
        {
        }

        private bool CanSaveProjectAs()
        {
            return true;
        }

        private void SaveProjectAs()
        {

        }

        private bool CanCloseProject()
        {
            return true;
        }

        private void CloseProject()
        {

        }

        private bool CanExit()
        {
            return true;
        }

        private void Exit()
        {

        }
    }
}
