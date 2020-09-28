using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KursProject.Commands.CommandForRegisterTrainer
{
    class RegistrationTrainer : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RegistrationTrainer(Action<object> execute, Func<object, bool> canExecute = null)
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
                        Trainer trainer1 = (Trainer)parameter;
                        int rand = value.Next(0, 100000);
                        var trainers = db.Trainer.ToList();
                    Found:
                        foreach (var c in trainers)
                        {
                            if (c.id == rand)
                            {
                                rand = value.Next(0, 100000);
                                goto Found;
                            }
                        }
                        foreach (var c in trainers)
                        {
                            if (c.Login == trainer1.Login)
                            {
                                throw new Exception("Такой логин уже используется");
                            }
                        }
                        if(trainer1.DataTrainer.BarbellSquat < trainer1.DataTrainer.Weight*1.4 && trainer1.DataTrainer.Deadlift < trainer1.DataTrainer.Weight * 1.8 &&
                            trainer1.DataTrainer.BenchPress < trainer1.DataTrainer.Weight*1.2 && trainer1.DataTrainer.Pullups < 10)
                            throw new Exception("Вы слабоваты для тренера");
                        trainer1.id = rand;
                        db.DataTrainer.Add(trainer1.DataTrainer);
                        db.Trainer.Add(trainer1);
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("значения введены неверно, перепроверьте пожалуйсто");
                        transaction.Rollback();
                    }
                }
            }

        }
    }
}
