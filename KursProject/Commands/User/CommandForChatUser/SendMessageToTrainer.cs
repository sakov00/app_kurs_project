using KursProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KursProject.Commands
{
    class SendMessageToTrainer : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public SendMessageToTrainer(Action<object> execute, Func<object, bool> canExecute = null)
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
                    messages.Who_sender = WorkUserWindowViewModel.client.id;
                    messages.Who_recipient = int.Parse(WindowOfViews.ChatUser.comboBox.Text);
                    messages.messages1 = WindowOfViews.ChatUser.messageText.Text;
                    messages.Date = DateTime.Now;
                    db.Messages.Add(messages);
                    db.SaveChanges();
                    ChatUserViewModel.lol.ChoiseClient = messages.Who_recipient.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
