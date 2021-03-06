﻿using KursProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KursProject.Commands.CommandForEditUser
{
    class ReturnDataUser : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public ReturnDataUser(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }
        public void Execute(object parameter)
        {
            using (BD_KURSACH_Entities db = new BD_KURSACH_Entities())
            {
                try
                {
                    WindowOfViews.EditDataUser.FirstName.Text = WorkUserWindowViewModel.client.FirstName;
                    WindowOfViews.EditDataUser.SecondName.Text = WorkUserWindowViewModel.client.SecondName;
                    WindowOfViews.EditDataUser.Login.Text = WorkUserWindowViewModel.client.Login;
                    WindowOfViews.EditDataUser.Password.Text = WorkUserWindowViewModel.client.Password;
                    WindowOfViews.EditDataUser.Weight.Text = WorkUserWindowViewModel.client.DataClient.Weight.ToString();
                    WindowOfViews.EditDataUser.Height.Text = WorkUserWindowViewModel.client.DataClient.Height.ToString();
                    WindowOfViews.EditDataUser.BodyType.Text = WorkUserWindowViewModel.client.DataClient.Bodytype.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
