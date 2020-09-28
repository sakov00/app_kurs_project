using KursProject.ViewModels;
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
    class SendMessageToUser : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public SendMessageToUser(Action<object> execute, Func<object, bool> canExecute = null)
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
                    Messages messages = new Messages();
                    Random value = new Random();
                    messages.id_Message = value.Next(0, 10000000);
                    messages.Who_sender = WorkTrainerWindowViewModel.trainer.id;
                    messages.Who_recipient = int.Parse(WindowOfViews.ChatTrainer.comboBox.Text);
                    messages.messages1 = WindowOfViews.ChatTrainer.messageText.Text;
                    messages.Date = DateTime.Now;
                    db.Messages.Add(messages);
                    db.SaveChanges();
                    ChatTrainerViewModel.lol.ChoiseClient = messages.Who_recipient.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
