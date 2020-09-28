using KursProject.Commands;
using KursProject.Commands.CommandForRegisterUser;
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
    class RegisterUserViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        private RegistrationUser Registration_User_Command;
        public RegistrationUser Registration_User_Click
        {
            get
            {
                return Registration_User_Command ??
                  (Registration_User_Command = new RegistrationUser(obj => {
                  }));
            }
        }

        private Client selectedclient=new Client();
        private DataClient selecteddataclient = new DataClient();
        public Client SelectedClient
        {
            get { return selectedclient; }
            set
            {
                selectedclient = value;
                OnPropertyChanged("SelectedClient");
            }
        }
        public string FirstName
        {
            get { return selectedclient.FirstName; }
            set
            {
                selectedclient.FirstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        public string SecondName
        {
            get { return selectedclient.SecondName; }
            set
            {
                selectedclient.SecondName = value;
                OnPropertyChanged("SecondName");
            }
        }
        public string Login
        {
            get { return selectedclient.Login; }
            set
            {
                selectedclient.Login = value;
                OnPropertyChanged("Login");
            }
        }
        public string Password
        {
            get { return selectedclient.Password; }
            set
            {
                selectedclient.Password = value;
                OnPropertyChanged("Password");
            }
        }
        public int Weight
        {
            get { return selecteddataclient.Weight; }
            set
            {
                selectedclient.DataClient = selecteddataclient;
                selecteddataclient.Weight = value;
                OnPropertyChanged("Weight");
            }
        }
        public int Height
        {
            get { return selecteddataclient.Height; }
            set
            {
                selectedclient.DataClient = selecteddataclient;
                selecteddataclient.Height = value;
                OnPropertyChanged("Height");
            }
        }
        public string Bodytype
        {
            get { return selecteddataclient.Bodytype; }
            set
            {
                selectedclient.DataClient = selecteddataclient;
                selecteddataclient.Bodytype = value;
                OnPropertyChanged("BodyType");
            }
        }
    }
}
