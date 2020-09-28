using KursProject.Commands;
using KursProject.Commands.CommandForEditUser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KursProject.ViewModels
{
    class ChatUserViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public ChatUserViewModel()
        {
            client = WorkUserWindowViewModel.client;
            lol = this;
            //Thread checkUserBlock = new Thread(new ThreadStart(ThreadFunc));
            //checkUserBlock.Start();
        }
        private SendMessageToTrainer Send_Message_Command;
        public SendMessageToTrainer Send_Message_Click
        {
            get
            {
                return Send_Message_Command ??
                  (Send_Message_Command = new SendMessageToTrainer(obj => { }));
            }
        }
        public static ChatUserViewModel lol;
        Client client = new Client();
        public static string[] GetTrainers()
        {
            using (BD_KURSACH_Entities db = new BD_KURSACH_Entities())
            {
                string[] mas = new string[db.Trainer.ToList().Count()];
                for (int i = 0; i < db.Trainer.ToList().Count(); i++)
                {
                    mas[i] = db.Trainer.ToList().ElementAt(i).id.ToString();
                }
                return mas;
            }
        }
        public string FirstName
        {
            get { return client.FirstName; }
            set
            {
                client.FirstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        public string SecondName
        {
            get { return client.SecondName; }
            set
            {
                client.SecondName = value;
                OnPropertyChanged("SecondName");
            }
        }
        public string[] SelectedList
            {
            get { return GetTrainers();}
            set
            {
                OnPropertyChanged("SelectedList");
            }
            }
        private string id;
        public string ChoiseClient
        {
            get { return id;}
            set
            {
                id = value;
                OnPropertyChanged("ChoiseClient");
                ListMessage = GetMessages();
            }
        }
        private string Messages;
        public string ListMessage
        {
            get { return GetMessages();}
            set
            {
                Messages = value;
                OnPropertyChanged("ListMessage");
            }
        }
        //private void ThreadFunc()
        //{
        //    while (true)
        //    {
        //        Thread.Sleep(5000);
        //        GetMessages();
        //    }
        //}
        private string GetMessages()
        {
            if (WindowOfViews.ChatUser != null)
            {
                Messages = "";
                using (BD_KURSACH_Entities db = new BD_KURSACH_Entities())
                {
                    var sortmessages = (from u in db.Messages.ToList()
                                        orderby u.Date ascending
                                        select u).Where(i => i.Who_recipient == int.Parse(id) && i.Who_sender.ToString() == WorkUserWindowViewModel.client.id.ToString() ||
                                        i.Who_recipient == WorkUserWindowViewModel.client.id && i.Who_sender == int.Parse(id));

                    for (int i = 0; i < sortmessages.Count(); i++)
                    {
                        Messages += sortmessages.ElementAt(i).Date + " " + sortmessages.ElementAt(i).messages1 + Environment.NewLine;
                    }
                }
            }
            return Messages;
        }
    }
}