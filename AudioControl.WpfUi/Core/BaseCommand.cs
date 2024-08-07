﻿using System;
using System.Windows.Input;

namespace AudioControl.WpfUi.Core
{
    public class BaseCommand : ICommand
    {
        private Action<object> _execute;

        private Func<object, bool>? _canExecute;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; } 
            remove { CommandManager.RequerySuggested -= value; }
        }

        public BaseCommand(Action<object> execute, Func<object, bool>? canExecute = null)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public virtual void Execute(object? parameter)
        {
            _execute(parameter);
        }
    }
}
