using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KursProject.Commands.CommandForEditUser
{
    class EditUserForExercises : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public EditUserForExercises(Action<object> execute, Func<object, bool> canExecute = null)
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
            using (BD_KURSACH_Entities db = new BD_KURSACH_Entities())
            {
                try
                {
                    int num = 0;
                    var clients = db.Client.ToList();
                    TextBox text = (TextBox)parameter;
                    var exer = db.ExercisesForClient.Where(i => i.Id_client.ToString() == text.Text).ToList();

                    exer.ElementAt(num).First_Day = WindowOfViews.EditUser.FirstDayFirstExercise.Text;
                    exer.ElementAt(num).Second_Day = WindowOfViews.EditUser.SecondDayFirstExercise.Text;
                    exer.ElementAt(num).Third_Day = WindowOfViews.EditUser.ThirdDayFirstExercise.Text;
                    num++;
                    exer.ElementAt(num).First_Day =WindowOfViews.EditUser.FirstDaySecondExercise.Text; 
                    exer.ElementAt(num).Second_Day =WindowOfViews.EditUser.SecondDaySecondExercise.Text; 
                    exer.ElementAt(num).Third_Day=WindowOfViews.EditUser.ThirdDaySecondExercise.Text; 
                    num++;
                    exer.ElementAt(num).First_Day =WindowOfViews.EditUser.FirstDayThirdExercise.Text; 
                    exer.ElementAt(num).Second_Day =WindowOfViews.EditUser.SecondDayThirdExercise.Text; 
                    exer.ElementAt(num).Third_Day=WindowOfViews.EditUser.ThirdDayThirdExercise.Text; 
                    num++;
                    exer.ElementAt(num).First_Day =WindowOfViews.EditUser.FirstDayFourthExercise.Text; 
                    exer.ElementAt(num).Second_Day =WindowOfViews.EditUser.SecondDayFourthExercise.Text; 
                    exer.ElementAt(num).Third_Day=WindowOfViews.EditUser.ThirdDayFourthExercise.Text; 
                    num++;
                    exer.ElementAt(num).First_Day =WindowOfViews.EditUser.FirstDayFifthExercise.Text; 
                    exer.ElementAt(num).Second_Day =WindowOfViews.EditUser.SecondDayFifthExercise.Text; 
                    exer.ElementAt(num).Third_Day=WindowOfViews.EditUser.ThirdDayFifthExercise.Text; 
                    num++;
                    exer.ElementAt(num).First_Day =WindowOfViews.EditUser.FirstDaySixthExercise.Text; 
                    exer.ElementAt(num).Second_Day =WindowOfViews.EditUser.SecondDaySixthExercise.Text; 
                    exer.ElementAt(num).Third_Day=WindowOfViews.EditUser.ThirdDaySixthExercise.Text; 
                    db.SaveChanges();
                    MessageBox.Show("изменения прошли успешно");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
