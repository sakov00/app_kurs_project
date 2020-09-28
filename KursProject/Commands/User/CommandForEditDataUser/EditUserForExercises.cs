using KursProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KursProject.Commands.CommandForEditUser
{
    class EditDataUserCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public EditDataUserCommand(Action<object> execute, Func<object, bool> canExecute = null)
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
                    WorkUserWindowViewModel.client.FirstName = WindowOfViews.EditDataUser.FirstName.Text;
                    WorkUserWindowViewModel.client.SecondName = WindowOfViews.EditDataUser.SecondName.Text;
                    WorkUserWindowViewModel.client.Login = WindowOfViews.EditDataUser.Login.Text;
                    WorkUserWindowViewModel.client.Password = WindowOfViews.EditDataUser.Password.Text;
                    WorkUserWindowViewModel.client.DataClient.Weight = int.Parse(WindowOfViews.EditDataUser.Weight.Text);
                    WorkUserWindowViewModel.client.DataClient.Height = int.Parse(WindowOfViews.EditDataUser.Height.Text);
                    WorkUserWindowViewModel.client.DataClient.Bodytype = WindowOfViews.EditDataUser.BodyType.Text;
                    db.Entry(WorkUserWindowViewModel.client).State = EntityState.Modified;
                    db.Entry(WorkUserWindowViewModel.client.DataClient).State = EntityState.Modified;
                    db.SaveChanges();
                    MessageBox.Show("изменения прошли успешно");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
