using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KursProject.Commands.CommandForRegisterUser
{
    class RegistrationUser : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RegistrationUser(Action<object> execute, Func<object, bool> canExecute = null)
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
            this.execute(parameter);
            using (BD_KURSACH_Entities db = new BD_KURSACH_Entities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        Random value = new Random();
                        Client client1 = (Client)parameter;
                        int rand = value.Next(0, 100000);
                        var clients = db.Client.ToList();
                    Found:
                        foreach (var c in clients)
                        {
                            if (c.id == rand)
                            {
                                rand = value.Next(0, 100000);
                                goto Found;
                            }
                        }
                        foreach (var c in clients)
                        {
                            if (c.Login == client1.Login)
                            {
                                throw new Exception("Такой логин уже используется");
                            }
                        }
                        client1.id = rand;
                        List<ExercisesForClient> lol = GenerateExercises(parameter);
                        db.Client.Add(client1);
                        db.DataClient.Add(client1.DataClient);
                        db.ExercisesForClient.AddRange(lol);
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
        }
        private List<ExercisesForClient> GenerateExercises(object parameter)
        {
            List<ExercisesForClient> list = new List<ExercisesForClient>();
            using (BD_KURSACH_Entities db = new BD_KURSACH_Entities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        Random value = new Random();
                        int rand = value.Next(0, 8);
                        Client client1 = (Client)parameter;
                        for (int k = 0; k < 6; k++)
                        {
                            list.Add(new ExercisesForClient());
                            list.ElementAt(k).Number = value.Next(0, 10000);
                            list.ElementAt(k).Id_client = client1.id;
                        }
                        string First_Day="";
                        string Second_Day="";
                        string Third_Day="";
                        var exercises = db.Exercises.ToList();
                        string[] GroupMus = Mas();
                        int i = 0;
                        int firstday = 0;
                        int secondday = 0;
                        int thirdday = 0;
                        if (rand >= 6)
                            rand = 6;
                        while(firstday < 6 || secondday < 6 || thirdday < 6)
                        foreach(var exer in exercises)
                        {
                            if (GroupMus[rand] == exer.MuscleGroups)
                            {
                                First_Day += exer.Exercises1 + ", ";
                                firstday++;
                                if (firstday == 2 || firstday == 4 || firstday == 6)
                                    GroupMus[rand] = null;
                            }
                            if (GroupMus[rand+1] == exer.MuscleGroups)
                            {
                                Second_Day += exer.Exercises1 + ", ";
                                secondday++;
                                if (secondday == 2 || secondday == 4 || secondday == 6)
                                    GroupMus[rand+1] = null;
                            }
                            if (GroupMus[rand+2] == exer.MuscleGroups)
                            {
                                Third_Day += exer.Exercises1 + ", ";
                                thirdday++;
                                if (thirdday ==2 || thirdday == 4 || thirdday==6)
                                    GroupMus[rand+2] = null;
                            }
                            if (GroupMus[rand] == null && GroupMus[rand + 1] == null && GroupMus[rand + 2] == null)
                            {
                            rand += 3;
                                if (rand >= 6)
                                    rand = 6;
                                    if (GroupMus[8] == null && i == 0)
                                    {
                                        i++;
                                        rand = 0;
                                    }
                            }                                
                        }
                        string[] first = First_Day.Split(new char[] { ',' });
                        string[] second = Second_Day.Split(new char[] { ',' });
                        string[] third = Third_Day.Split(new char[] { ',' });
                        for (int k = 0; k < 6; k++)
                        {
                            list.ElementAt(k).First_Day = first[k];
                            list.ElementAt(k).Second_Day = second[k];
                            list.ElementAt(k).Third_Day = third[k];
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("не удалось сгенерировать программу тренировок");
                        transaction.Rollback();
                    }
                }
            }
            return list;
        }
        private string[] Mas()
        {
            using (BD_KURSACH_Entities db = new BD_KURSACH_Entities())
            {
                var exercises = db.Exercises.ToList();
                string[] GM = new string[9];
                int count = 0;
                foreach (var exer in exercises)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        if (GM[i] == exer.MuscleGroups)
                            break;
                        if (exer.MuscleGroups != GM[count]&& count==i)
                        {
                            GM[count] = exer.MuscleGroups;
                            count++;
                            break;
                        }
                    }
                    if (count == 9)
                        break;
                }
                return GM;
            }
        }
    }
}
