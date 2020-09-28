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
    class ChatTrainerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public ChatTrainerViewModel()
        {
            trainer = WorkTrainerWindowViewModel.trainer;
            lol = this;
            //Thread checkUserBlock = new Thread(new ThreadStart(ThreadFunc));
            //checkUserBlock.Start();
        }
        public static ChatTrainerViewModel lol;
        private SendMessageToUser Send_Message_Command;
        public SendMessageToUser Send_Message_Click
        {
            get
            {
                return Send_Message_Command ??
                  (Send_Message_Command = new SendMessageToUser(obj => { }));
            }
        }

        public static string[] GetClientsGroup()
        {
            using (BD_KURSACH_Entities db = new BD_KURSACH_Entities())
            {
                string[] mas = new string[db.Client.ToList().Count()];
                for (int i = 0; i < db.Client.ToList().Count(); i++)
                {
                    mas[i] = db.Client.ToList().ElementAt(i).id.ToString();
                }
                return mas;
            }
        }

        private static Trainer trainer = new Trainer();
        public string FirstName
        {
            get { return trainer.FirstName; }
            set
            {
                trainer.FirstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        public string SecondName
        {
            get { return trainer.SecondName; }
            set
            {
                trainer.SecondName = value;
                OnPropertyChanged("SecondName");
            }
        }
        public string[] SelectedList
            {
            get { return GetClientsGroup();}
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
            if (WindowOfViews.ChatTrainer != null)
            {
                Messages = "";
                using (BD_KURSACH_Entities db = new BD_KURSACH_Entities())
                {
                    var sortmessages = (from u in db.Messages.ToList()
                                        orderby u.Date ascending
                                        select u).Where(i => i.Who_recipient == int.Parse(id) && i.Who_sender.ToString() == WorkTrainerWindowViewModel.trainer.id.ToString()||
                                        i.Who_recipient == WorkTrainerWindowViewModel.trainer.id && i.Who_sender == int.Parse(id));
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