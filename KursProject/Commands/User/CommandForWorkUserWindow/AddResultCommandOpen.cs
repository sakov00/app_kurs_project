using KursProject.ViewModels;
using KursProject.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KursProject.Commands.CommandForWorkTrainerWindow
{
    class AddResultCommandOpen : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public AddResultCommandOpen(Action<object> execute, Func<object, bool> canExecute = null)
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
            WindowOfViews.AddResult = new AddResult();
            getExercises();
            WindowOfViews.AddResult.Show();
        }
        private static void getExercises()
        {
            using (BD_KURSACH_Entities db = new BD_KURSACH_Entities())
            {
                try
                {
                    int num = 0;

                    var exer = db.ExercisesForClient.Where(i => i.Id_client == WorkUserWindowViewModel.client.id).ToList();

                    WindowOfViews.AddResult.FirstDayFirstExercise.Content = exer.ElementAt(num).First_Day;
                    WindowOfViews.AddResult.SecondDayFirstExercise.Content = exer.ElementAt(num).Second_Day;
                    WindowOfViews.AddResult.ThirdDayFirstExercise.Content = exer.ElementAt(num).Third_Day;
                    num++;
                    WindowOfViews.AddResult.FirstDaySecondExercise.Content = exer.ElementAt(num).First_Day;
                    WindowOfViews.AddResult.SecondDaySecondExercise.Content = exer.ElementAt(num).Second_Day;
                    WindowOfViews.AddResult.ThirdDaySecondExercise.Content = exer.ElementAt(num).Third_Day;
                    num++;
                    WindowOfViews.AddResult.FirstDayThirdExercise.Content = exer.ElementAt(num).First_Day;
                    WindowOfViews.AddResult.SecondDayThirdExercise.Content = exer.ElementAt(num).Second_Day;
                    WindowOfViews.AddResult.ThirdDayThirdExercise.Content = exer.ElementAt(num).Third_Day;
                    num++;
                    WindowOfViews.AddResult.FirstDayFourthExercise.Content = exer.ElementAt(num).First_Day;
                    WindowOfViews.AddResult.SecondDayFourthExercise.Content = exer.ElementAt(num).Second_Day;
                    WindowOfViews.AddResult.ThirdDayFourthExercise.Content = exer.ElementAt(num).Third_Day;
                    num++;
                    WindowOfViews.AddResult.FirstDayFifthExercise.Content = exer.ElementAt(num).First_Day;
                    WindowOfViews.AddResult.SecondDayFifthExercise.Content = exer.ElementAt(num).Second_Day;
                    WindowOfViews.AddResult.ThirdDayFifthExercise.Content = exer.ElementAt(num).Third_Day;
                    num++;
                    WindowOfViews.AddResult.FirstDaySixthExercise.Content = exer.ElementAt(num).First_Day;
                    WindowOfViews.AddResult.SecondDaySixthExercise.Content = exer.ElementAt(num).Second_Day;
                    WindowOfViews.AddResult.ThirdDaySixthExercise.Content = exer.ElementAt(num).Third_Day;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
