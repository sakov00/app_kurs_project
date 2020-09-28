using KursProject.Commands.CommandForGroupTrainer;
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
    class WorkTrainerGroupViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public WorkTrainerGroupViewModel()
        {
                trainer = WorkTrainerWindowViewModel.trainer;
        }

        private AddUser Add_User_For_Group_Command;
        public AddUser Add_User_For_Group_Click
        {
            get
            {
                return Add_User_For_Group_Command ??
                  (Add_User_For_Group_Command = new AddUser(obj => { }));
            }
        }

        private CreateGroup Create_Group_Command;
        public CreateGroup Create_Group_Click
        {
            get
            {
                return Create_Group_Command ??
                  (Create_Group_Command = new CreateGroup(obj => { }));
            }
        }
        public List<Client> SelectedList
        {
            get { return GetClients(); }
            set
            {
                OnPropertyChanged("SelectedList");
            }
        }
        public List<Client> SelectedListGroup
        {
            get { return GetClientsGroup(); }
            set
            {
                OnPropertyChanged("SelectedListGroup");
            }
        }
        public static List<Client> GetClients()
        {
            using (BD_KURSACH_Entities db = new BD_KURSACH_Entities())
                return db.Client.ToList();
        }
        public static List<Client> GetClientsGroup()
        {
            using (BD_KURSACH_Entities db = new BD_KURSACH_Entities())
            {
                var clients = db.Client.ToList();
                List<Client> ClientGroup = new List<Client>();
                foreach (var c in clients)
                {
                    if (c.Number_Group.ToString() == trainer.Number_Group.ToString())
                    {
                        ClientGroup.Add(c);
                    }
                }
                return ClientGroup;
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
    }
}
