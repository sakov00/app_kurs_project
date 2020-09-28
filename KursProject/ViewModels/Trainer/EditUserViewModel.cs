using KursProject.Commands.CommandForEditUser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KursProject.ViewModels
{
    class EditUserViewModel : INotifyPropertyChanged
    {
            public event PropertyChangedEventHandler PropertyChanged;
            public void OnPropertyChanged([CallerMemberName]string prop = "")
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
            public EditUserViewModel()
            {
                trainer = WorkTrainerWindowViewModel.trainer;
            }

            private EditUserForExercises Edit_User_For_Exercises_Command;
            public EditUserForExercises Edit_User_For_Exercises_Click
            {
            get
            {
                return Edit_User_For_Exercises_Command ??
                  (Edit_User_For_Exercises_Command = new EditUserForExercises(obj => { }));
            }
            }

            private GetExercises Get_Exercises_Command;
            public GetExercises Get_Exercises_Click
        {
            get
            {
                return Get_Exercises_Command ??
                  (Get_Exercises_Command = new GetExercises(obj => { }));
            }
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
            public List<Client> SelectedListGroup
            {
            get { return GetClientsGroup(); }
            set
            {
                OnPropertyChanged("SelectedListGroup");
            }
            }
    }
}