using KursProject.Commands.CommandForRegisterTrainer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KursProject.ViewModels
{
    class RegisterTrainerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        private RegistrationTrainer Registration_Trainer_Command;
        public RegistrationTrainer Registration_Trainer_Click
        {
            get
            {
                return Registration_Trainer_Command ??
                  (Registration_Trainer_Command = new RegistrationTrainer(obj => { }));
            }
        }
        private Trainer selectedtrainer = new Trainer();
        private DataTrainer selecteddatatrainer = new DataTrainer();
        public Trainer SelectedTrainer
        {
            get { return selectedtrainer; }
            set
            {
                selectedtrainer = value;
                OnPropertyChanged("SelectedTrainer");
            }
        }
        public string FirstName
        {
            get { return selectedtrainer.FirstName; }
            set
            {
                selectedtrainer.FirstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        public string SecondName
        {
            get { return selectedtrainer.SecondName; }
            set
            {
                selectedtrainer.SecondName = value;
                OnPropertyChanged("SecondName");
            }
        }
        public string Login
        {
            get { return selectedtrainer.Login; }
            set
            {
                selectedtrainer.Login = value;
                OnPropertyChanged("Login");
            }
        }
        public string Password
        {
            get { return selectedtrainer.Password; }
            set
            {
                selectedtrainer.Password = value;
                OnPropertyChanged("Password");
            }
        }
        public int Weight
        {
            get { return selecteddatatrainer.Weight; }
            set
            {
                selectedtrainer.DataTrainer = selecteddatatrainer;
                selecteddatatrainer.Weight = value;
                OnPropertyChanged("Weight");
            }
        }
        public int Height
        {
            get { return selecteddatatrainer.Height;}
            set
            {
                selectedtrainer.DataTrainer = selecteddatatrainer;
                selecteddatatrainer.Height = value;
                OnPropertyChanged("Height");
            }
        }
        public string BodyType
        {
            get { return selecteddatatrainer.BodyType; }
            set
            {
                selectedtrainer.DataTrainer = selecteddatatrainer;
                selecteddatatrainer.BodyType = value;
                OnPropertyChanged("BodyType");
            }
        }
        public int BarbellSquat
        {
            get { return selecteddatatrainer.BarbellSquat; }
            set
            {
                selectedtrainer.DataTrainer = selecteddatatrainer;
                selecteddatatrainer.BarbellSquat = value;
                OnPropertyChanged("BarbellSquat");
            }
        }
        public int Deadlift
        {
            get { return selecteddatatrainer.Deadlift; }
            set
            {
                selectedtrainer.DataTrainer = selecteddatatrainer;
                selecteddatatrainer.Deadlift = value;
                OnPropertyChanged("Deadlift");
            }
        }
        public int BenchPress
        {
            get { return selecteddatatrainer.BenchPress; }
            set
            {
                selectedtrainer.DataTrainer = selecteddatatrainer;
                selecteddatatrainer.BenchPress = value;
                OnPropertyChanged("BenchPress");
            }
        }
        public int Pullups
        {
            get { return selecteddatatrainer.Pullups; }
            set
            {
                selectedtrainer.DataTrainer = selecteddatatrainer;
                selecteddatatrainer.Pullups = value;
                OnPropertyChanged("Pullups");
            }
        }
    }
}
