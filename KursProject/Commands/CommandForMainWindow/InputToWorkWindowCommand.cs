using KursProject.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KursProject.Commands.CommandForMainWindow
{
    class InputToWorkWindowCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public InputToWorkWindowCommand(Action<object> execute, Func<object, bool> canExecute = null)
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
            this.execute(parameter);
            using (BD_KURSACH_Entities db = new BD_KURSACH_Entities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        string[] text = (string[])parameter;
                        var clients = db.Client.ToList();
                        var trainers = db.Trainer.ToList();
                        bool Fine = false;
                        foreach (var c in clients)
                        {
                            if (c.Login.Replace(" ", "") == text[0] && c.Password.Replace(" ", "") == text[1])
                            {
                                WindowOfViews.WorkUserWindow = new WorkUserWindow();
                                WindowOfViews.WorkUserWindow.Show();
                                Fine = true;
                            }
                        }
                        foreach (var c in trainers)
                        {
                            if (c.Login.Replace(" ", "") == text[0] && c.Password.Replace(" ", "") == text[1])
                            {
                                WindowOfViews.WorkTrainerWindow = new WorkTrainerWindow();
                                WindowOfViews.WorkTrainerWindow.Show();
                                Fine = true;
                            }
                        }
                        if (Fine == true)
                        {
                            transaction.Commit();
                            Application.Current.MainWindow.Close();
                        }
                        if (Fine == false)
                            MessageBox.Show("Вы неправильно ввели данные");
                        }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        transaction.Rollback();
                    }
                }
            }


        }
    }
}
