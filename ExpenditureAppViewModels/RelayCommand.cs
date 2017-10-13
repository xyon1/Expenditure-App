﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExpenditureAppViewModel
{
    public class RelayCommand : ICommand
    {
        private Action task;

        public RelayCommand(Action action)
        {
            task = action;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            task();
        }
    }
}