using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfUtils
{
    /// <summary>
    /// This class provides a convienient way to support data bindning to view model functions
    /// </summary>
    public class RelayCommand : ICommand 
    { 
        #region Fields
        // provided delegate that is invoked by the ICommand
        readonly Action<object> _execute; 
        // provided delegate that determines if the delegate can be invoked
        readonly Predicate<object> _canExecute; 
        #endregion // Fields 
        
        #region Constructors 
        public RelayCommand(Action<object> execute) : this(execute, null) { } 
        public RelayCommand(Action<object> execute, Predicate<object> canExecute) 
        { 
            if (execute == null) 
                throw new ArgumentNullException("execute"); 
            _execute = execute; 
            _canExecute = canExecute; 
        } 
        #endregion // Constructors 
        
        #region ICommand Members 
        [DebuggerStepThrough] 
        public bool CanExecute(object parameter) 
        { 
            return _canExecute == null ? true : _canExecute(parameter); 
        } 
        
        public event EventHandler CanExecuteChanged 
        { 
            add { CommandManager.RequerySuggested += value; } 
            remove { CommandManager.RequerySuggested -= value; } 
        } 
        
        public void Execute(object parameter) 
        { 
            _execute(parameter); 
        } 
        #endregion // ICommand Members 
    }
}
