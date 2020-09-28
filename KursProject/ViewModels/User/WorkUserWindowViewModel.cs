using KursProject.Commands.CommandForWorkTrainerWindow;
using KursProject.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KursProject.ViewModels
{
    class WorkUserWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public WorkUserWindowViewModel()
        {
            using (BD_KURSACH_Entities db = new BD_KURSACH_Entities())
            {
                client = db.Client.Include("DataClient").Where(i => i.Login.Replace(" ", "") == ((MainWindow)Application.Current.MainWindow).Login.Text &&
              i.Password.Replace(" ", "") == ((MainWindow)Application.Current.MainWindow).Password.Text).FirstOrDefault();
            }
        }
        private WorkGroupWithTrainer Work_Group_With_Trainer_Command;
        public WorkGroupWithTrainer Work_Group_With_Trainer_Click
        {
            get
            {
                return Work_Group_With_Trainer_Command ??
                  (Work_Group_With_Trainer_Command = new WorkGroupWithTrainer(obj => { }));
            }
        }

        private ChatWithTrainerCommand Chat_With_Trainer_Command;
        public ChatWithTrainerCommand Chat_With_Trainer_Click
        {
            get
            {
                return Chat_With_Trainer_Command ??
                  (Chat_With_Trainer_Command = new ChatWithTrainerCommand(obj => { }));
            }
        }

        private EditDataUserCommand Edit_Data_Client_Command;
        public EditDataUserCommand Edit_Data_Client_Click
        {
            get
            {
                return Edit_Data_Client_Command ??
                  (Edit_Data_Client_Command = new EditDataUserCommand(obj => { }));
            }
        }

        private CheckMyProgressCommand Check_My_Progress_Command;
        public CheckMyProgressCommand Check_My_Progress_Click
        { 
            get
            {
                return Check_My_Progress_Command ??
                  (Check_My_Progress_Command = new CheckMyProgressCommand(obj => { }));
            }
        }

        private AddResultCommandOpen Add_Result_Open_Command;
        public AddResultCommandOpen Add_Result_Open_Click
        {
            get
            {
                return Add_Result_Open_Command ??
                  (Add_Result_Open_Command = new AddResultCommandOpen(obj => { }));
            }
        }


        public static Client client = new Client();

        public Client MyClient
        {
            get { return client; }
            set
            {
                client = value;
                OnPropertyChanged("MyClient");
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
        public double Progress
        {
            get { return GetProgress(); }
            set
            {
                client.DataClient.Progress = value;
                OnPropertyChanged("Progress");
            }
        }
        public List<ExercisesForClient> GetListExecises
        {
            get { return GetExercises();}
            set
            {
                OnPropertyChanged("GetListExecises");
            }
        }
        public static List<ExercisesForClient> GetExercises()
        {
            using (BD_KURSACH_Entities db = new BD_KURSACH_Entities())
                return db.ExercisesForClient.ToList();
        }

        private static double GetProgress()
        {
            double progress11 = 1;
            double progress12 = 1;
            double progress13 = 1;
            double progress21 = 1;
            double progress22 = 1;
            double progress23 = 1;
            int crossexer = 0;
            double progress = 0;
            using (BD_KURSACH_Entities db = new BD_KURSACH_Entities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    var exer = db.ResultExercises.Where(i => i.Id_Client == WorkUserWindowViewModel.client.id).ToList();
                    if (exer.Count() != 0)
                    try
                    {
                        
                        var sortedexer = from u in exer
                                         orderby u.Date descending
                                         select u;
                        if (sortedexer.FirstOrDefault().First_Day_Quantity != "" && sortedexer.FirstOrDefault().Second_Day_Quantity != "" && sortedexer.FirstOrDefault().Third_Day_Quantity != "")
                            crossexer = 6;
                        if ( sortedexer.FirstOrDefault().First_Day_Quantity == "" || sortedexer.FirstOrDefault().Second_Day_Quantity == "" || sortedexer.FirstOrDefault().Third_Day_Quantity == "")
                            crossexer = 12;

                            if (sortedexer.FirstOrDefault().First_Day_Quantity != "" && sortedexer.FirstOrDefault().Second_Day_Quantity == "" && sortedexer.FirstOrDefault().Third_Day_Quantity == "")
                                crossexer += 6;
                            if (sortedexer.FirstOrDefault().First_Day_Quantity == "" && sortedexer.FirstOrDefault().Second_Day_Quantity != "" && sortedexer.FirstOrDefault().Third_Day_Quantity == "")
                                crossexer += 6; 
                            if (sortedexer.FirstOrDefault().First_Day_Quantity == "" && sortedexer.FirstOrDefault().Second_Day_Quantity == "" && sortedexer.FirstOrDefault().Third_Day_Quantity != "")
                                crossexer += 6;

                            for (int i = 0; i < crossexer; i++)
                            {
                            if (sortedexer.ElementAt(i).First_Day_Quantity != "")
                            {
                                if (sortedexer.ElementAt(i).First_Day_Weight != "" && sortedexer.ElementAt(i).First_Day_Quantity != "")
                                {
                                    progress11 = int.Parse(sortedexer.ElementAt(i).First_Day_Weight);
                                    progress11 = progress11 * int.Parse(sortedexer.ElementAt(i).First_Day_Quantity) * 0.8;
                                }
                                if (sortedexer.ElementAt(i).First_Day_Weight == "" && sortedexer.ElementAt(i).First_Day_Quantity != "")
                                    progress11 = int.Parse(sortedexer.ElementAt(i).First_Day_Quantity) * 10;
                            }
                            if (sortedexer.ElementAt(i).Second_Day_Quantity != "")
                            {
                                if (sortedexer.ElementAt(i).Second_Day_Weight != "" && sortedexer.ElementAt(i).Second_Day_Quantity != "")
                                {
                                    progress12 = int.Parse(sortedexer.ElementAt(i).Second_Day_Weight);
                                    progress12 = progress12 * int.Parse(sortedexer.ElementAt(i).Second_Day_Quantity) * 0.8;
                                }
                                if (sortedexer.ElementAt(i).Second_Day_Weight == "" && sortedexer.ElementAt(i).Second_Day_Quantity != "")
                                    progress12 = int.Parse(sortedexer.ElementAt(i).Second_Day_Quantity) * 10;
                            }
                            if (sortedexer.ElementAt(i).Third_Day_Quantity != "")
                            {
                                if (sortedexer.ElementAt(i).Third_Day_Weight != "" && sortedexer.ElementAt(i).Third_Day_Quantity != "")
                                {
                                    progress13 = int.Parse(sortedexer.ElementAt(i).Third_Day_Weight);
                                    progress13 = progress13 * int.Parse(sortedexer.ElementAt(i).Third_Day_Quantity) * 0.8;
                                }
                                if (sortedexer.ElementAt(i).Third_Day_Weight == "" && sortedexer.ElementAt(i).Third_Day_Quantity != "")
                                    progress13 = int.Parse(sortedexer.ElementAt(i).Third_Day_Quantity) * 10;
                            }
                        }
                        //--------------------------------------------------------------------------------------------------------------------
                        for (int k = sortedexer.Count() - crossexer; k < sortedexer.Count(); k++)
                        {
                            if (sortedexer.ElementAt(k).First_Day_Quantity != "")
                            {
                                if (sortedexer.ElementAt(k).First_Day_Weight != "" && sortedexer.ElementAt(k).First_Day_Quantity != "")
                                {
                                    progress21 = int.Parse(sortedexer.ElementAt(k).First_Day_Weight);
                                    progress21 = progress21 * int.Parse(sortedexer.ElementAt(k).First_Day_Quantity) * 0.8;
                                }
                                if (sortedexer.ElementAt(k).First_Day_Weight == "" && sortedexer.ElementAt(k).First_Day_Quantity != "")
                                    progress21 = int.Parse(sortedexer.ElementAt(k).First_Day_Quantity) * 10;
                            }
                            if (sortedexer.ElementAt(k).Second_Day_Quantity != "")
                            {
                                if (sortedexer.ElementAt(k).Second_Day_Weight != "" && sortedexer.ElementAt(k).Second_Day_Quantity != "")
                                {
                                    progress22 = int.Parse(sortedexer.ElementAt(k).Second_Day_Weight);
                                    progress22 = progress22 * int.Parse(sortedexer.ElementAt(k).Second_Day_Quantity) * 0.8;
                                }
                                if (sortedexer.ElementAt(k).Second_Day_Weight == "" && sortedexer.ElementAt(k).Second_Day_Quantity != "")
                                    progress22 = int.Parse(sortedexer.ElementAt(k).Second_Day_Quantity) * 10;
                            }
                            if (sortedexer.ElementAt(k).Third_Day_Quantity != "")
                            {
                                if (sortedexer.ElementAt(k).Third_Day_Weight != "" && sortedexer.ElementAt(k).Third_Day_Quantity != "")
                                {
                                    progress23 = int.Parse(sortedexer.ElementAt(k).Third_Day_Weight);
                                    progress23 = progress23 * int.Parse(sortedexer.ElementAt(k).Third_Day_Quantity) * 0.8;
                                }
                                if (sortedexer.ElementAt(k).Third_Day_Weight == "" && sortedexer.ElementAt(k).Third_Day_Quantity != "")
                                    progress23 = int.Parse(sortedexer.ElementAt(k).Third_Day_Quantity) * 10;
                            }
                        }

                        if (progress21 != 1 && progress22 != 1 && progress23 != 1)
                            progress = (progress21 + progress22 + progress23) / (progress11 + progress12 + progress13);
                        if (progress21 == 1)
                            progress = (double)client.DataClient.Progress * (progress21 / progress11);
                        if (progress22 == 1)
                            progress = (double)client.DataClient.Progress * (progress22 / progress12);
                        if (progress23 == 1)
                            progress = (double)client.DataClient.Progress * (progress23 / progress13);
                        client.DataClient.Progress = Math.Round(progress, 2);
                        db.Entry(client.DataClient).State = EntityState.Modified;
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        transaction.Rollback();
                    }
                }
            }
            return Math.Round(progress, 2);
        }
    }
}
