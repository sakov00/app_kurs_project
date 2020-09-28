using KursProject.Commands.CommandForWorkTrainerWindow;
using KursProject.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KursProject.ViewModels
{
    class WorkTrainerWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public WorkTrainerWindowViewModel()
        {
            using (BD_KURSACH_Entities db = new BD_KURSACH_Entities())
            {
                trainer = db.Trainer.Include("DataTrainer").Include("Group").Where(i => i.Login.Replace(" ", "") == ((MainWindow)Application.Current.MainWindow).Login.Text &&
                 i.Password.Replace(" ", "") == ((MainWindow)Application.Current.MainWindow).Password.Text).FirstOrDefault();
            }
        }
        private WorkGroupForTrainer Work_Group_Command;
        public WorkGroupForTrainer Work_Group_Click
        {
            get
            {
                return Work_Group_Command ??
                  (Work_Group_Command = new WorkGroupForTrainer(obj => {}));
            }
        }

        private ChatUserCommand Chat_User_Command;
        public ChatUserCommand Chat_User_Click
        {
            get
            {
                return Chat_User_Command ??
                  (Chat_User_Command = new ChatUserCommand(obj => { }));
            }
        }
        private EditUserCommand Edit_Client_Command;
        public EditUserCommand Edit_Client_Click
        {
            get
            {
                return Edit_Client_Command ??
                  (Edit_Client_Command = new EditUserCommand(obj => { }));
            }
        }
        public List<Client> SelectedList
        {
            get { return lol(); }
            set
            {
                OnPropertyChanged("SelectedList");
            }
        }
        public static List<Client> lol()
        {
            using (BD_KURSACH_Entities db = new BD_KURSACH_Entities())
                return db.Client.ToList();
        }
        public static Trainer trainer = new Trainer();

        public Trainer MyTrainer
        {
            get { return trainer; }
            set
            {
                trainer = value;
                OnPropertyChanged("MyTrainer");
            }
        }
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
    }
}
