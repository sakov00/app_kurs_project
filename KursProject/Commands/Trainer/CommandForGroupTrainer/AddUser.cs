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

namespace KursProject.Commands.CommandForGroupTrainer
{
    class AddUser : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public AddUser(Action<object> execute, Func<object, bool> canExecute = null)
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
                try
                {
                    Client client = new Client();
                    var clients = db.Client.ToList();
                    TextBox text = (TextBox)parameter;
                    foreach (var c in clients)
                    {
                        if (c.id.ToString() == text.Text)
                        {
                            client = c;
                        }
                    }
                    Trainer trainer = WorkTrainerWindowViewModel.trainer;
                    client.Number_Group = trainer.Number_Group;
                    db.Entry(client).State = EntityState.Modified;
                    db.SaveChanges();
                    MessageBox.Show($"клиент добавлен в группу под номером {client.Number_Group}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
