using KursProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KursProject.Commands
{
    class AddResultCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public AddResultCommand(Action<object> execute, Func<object, bool> canExecute = null)
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
                    List<ResultExercises> listresult = new List<ResultExercises>();
                    var exer = db.ExercisesForClient.Where(i => i.Id_client == WorkUserWindowViewModel.client.id).ToList();
                    int num;
                    Random value = new Random();
                    for (num = 0;num<exer.Count;num++)
                    {
                        listresult.Add(new ResultExercises());
                        listresult.ElementAt(num).First_Day = exer.ElementAt(num).First_Day;
                        listresult.ElementAt(num).Second_Day = exer.ElementAt(num).Second_Day;
                        listresult.ElementAt(num).Third_Day = exer.ElementAt(num).Third_Day;
                        listresult.ElementAt(num).number = value.Next(0, 1000000);
                        listresult.ElementAt(num).Id_Client = exer.ElementAt(num).Id_client;
                        listresult.ElementAt(num).Date = DateTime.Now;
                    }
                    num = 0;
                    listresult.ElementAt(num).First_Day_Weight = WindowOfViews.AddResult.FirstDay1_Copy.Text;
                    listresult.ElementAt(num).First_Day_Quantity = WindowOfViews.AddResult.FirstDay1.Text;
                    listresult.ElementAt(num).Second_Day_Weight = WindowOfViews.AddResult.SecondDay1_Copy.Text;
                    listresult.ElementAt(num).Second_Day_Quantity = WindowOfViews.AddResult.SecondDay1.Text;
                    listresult.ElementAt(num).Third_Day_Weight = WindowOfViews.AddResult.ThirdDay1_Copy.Text;
                    listresult.ElementAt(num).Third_Day_Quantity = WindowOfViews.AddResult.ThirdDay1.Text;
                    num++;
                    listresult.ElementAt(num).First_Day_Weight = WindowOfViews.AddResult.FirstDay2_Copy.Text;
                    listresult.ElementAt(num).First_Day_Quantity =WindowOfViews.AddResult.FirstDay2.Text;
                    listresult.ElementAt(num).Second_Day_Weight = WindowOfViews.AddResult.SecondDay2_Copy.Text;
                    listresult.ElementAt(num).Second_Day_Quantity = WindowOfViews.AddResult.SecondDay2.Text;
                    listresult.ElementAt(num).Third_Day_Weight = WindowOfViews.AddResult.ThirdDay2.Text;
                    listresult.ElementAt(num).Third_Day_Quantity = WindowOfViews.AddResult.ThirdDay2.Text;
                    num++;
                    listresult.ElementAt(num).First_Day_Weight = WindowOfViews.AddResult.FirstDay3_Copy.Text;
                    listresult.ElementAt(num).First_Day_Quantity = WindowOfViews.AddResult.FirstDay3.Text;
                    listresult.ElementAt(num).Second_Day_Weight = WindowOfViews.AddResult.SecondDay3_Copy.Text;
                    listresult.ElementAt(num).Second_Day_Quantity = WindowOfViews.AddResult.SecondDay3.Text;
                    listresult.ElementAt(num).Third_Day_Weight = WindowOfViews.AddResult.ThirdDay3_Copy.Text;
                    listresult.ElementAt(num).Third_Day_Quantity = WindowOfViews.AddResult.ThirdDay3.Text;
                    num++;
                    listresult.ElementAt(num).First_Day_Weight = WindowOfViews.AddResult.FirstDay4_Copy.Text;
                    listresult.ElementAt(num).First_Day_Quantity = WindowOfViews.AddResult.FirstDay4.Text;
                    listresult.ElementAt(num).Second_Day_Weight = WindowOfViews.AddResult.SecondDay4_Copy.Text;
                    listresult.ElementAt(num).Second_Day_Quantity = WindowOfViews.AddResult.SecondDay4.Text;
                    listresult.ElementAt(num).Third_Day_Weight = WindowOfViews.AddResult.ThirdDay4_Copy.Text;
                    listresult.ElementAt(num).Third_Day_Quantity = WindowOfViews.AddResult.ThirdDay4.Text;
                    num++;
                    listresult.ElementAt(num).First_Day_Weight = WindowOfViews.AddResult.FirstDay5_Copy.Text;
                    listresult.ElementAt(num).First_Day_Quantity = WindowOfViews.AddResult.FirstDay5.Text;
                    listresult.ElementAt(num).Second_Day_Weight = WindowOfViews.AddResult.SecondDay5_Copy.Text;
                    listresult.ElementAt(num).Second_Day_Quantity =WindowOfViews.AddResult.SecondDay5.Text;
                    listresult.ElementAt(num).Third_Day_Weight = WindowOfViews.AddResult.ThirdDay5_Copy.Text;
                    listresult.ElementAt(num).Third_Day_Quantity = WindowOfViews.AddResult.ThirdDay5.Text;
                    num++;
                    listresult.ElementAt(num).First_Day_Weight = WindowOfViews.AddResult.FirstDay6_Copy.Text;
                    listresult.ElementAt(num).First_Day_Quantity = WindowOfViews.AddResult.FirstDay6.Text;
                    listresult.ElementAt(num).Second_Day_Weight = WindowOfViews.AddResult.SecondDay6_Copy.Text;
                    listresult.ElementAt(num).Second_Day_Quantity =WindowOfViews.AddResult.SecondDay6.Text;
                    listresult.ElementAt(num).Third_Day_Weight = WindowOfViews.AddResult.ThirdDay6_Copy.Text;
                    listresult.ElementAt(num).Third_Day_Quantity = (WindowOfViews.AddResult.ThirdDay6.Text);
                    num++;
                    bool fics = false;
                    int k;
                    for (int i = 0; i < exer.Count; i++)
                    {
                        if (listresult.ElementAt(i).First_Day_Weight != "" && !int.TryParse(listresult.ElementAt(i).First_Day_Weight, out k))
                            fics = true;
                        if (listresult.ElementAt(i).First_Day_Quantity != "" && !int.TryParse(listresult.ElementAt(i).First_Day_Quantity, out k))
                            fics = true;
                        if (listresult.ElementAt(i).Second_Day_Weight != "" && !int.TryParse(listresult.ElementAt(i).Second_Day_Weight, out k))
                            fics = true;
                        if (listresult.ElementAt(i).Second_Day_Quantity != "" && !int.TryParse(listresult.ElementAt(i).Second_Day_Quantity, out k))
                            fics = true;
                        if (listresult.ElementAt(i).Third_Day_Weight != "" && !int.TryParse(listresult.ElementAt(i).Third_Day_Weight, out k))
                            fics = true;
                        if (listresult.ElementAt(i).Third_Day_Quantity != "" && !int.TryParse(listresult.ElementAt(i).Third_Day_Quantity, out k))
                            fics = true;
                    }
                    if (fics==true)
                    {
                        throw new Exception("неверно введено значение");
                    }
                    if (fics==false)
                    {
                        db.ResultExercises.AddRange(listresult);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        //private static void getExercises()
        //{
        //    using (BD_KURSACH_Entities db = new BD_KURSACH_Entities())
        //    {
        //        try
        //        {
        //            int num = 0;

        //            var exer = db.ExercisesForClient.Where(i => i.Id_client == WorkUserWindowViewModel.client.id).ToList();

        //            WindowOfViews.AddResult.FirstDayFirstExercise.Content = exer.ElementAt(num).First_Day;
        //            WindowOfViews.AddResult.SecondDayFirstExercise.Content = exer.ElementAt(num).Second_Day;
        //            WindowOfViews.AddResult.ThirdDayFirstExercise.Content = exer.ElementAt(num).Third_Day;
        //            num++;
        //            WindowOfViews.AddResult.FirstDaySecondExercise.Content = exer.ElementAt(num).First_Day;
        //            WindowOfViews.AddResult.SecondDaySecondExercise.Content = exer.ElementAt(num).Second_Day;
        //            WindowOfViews.AddResult.ThirdDaySecondExercise.Content = exer.ElementAt(num).Third_Day;
        //            num++;
        //            WindowOfViews.AddResult.FirstDayThirdExercise.Content = exer.ElementAt(num).First_Day;
        //            WindowOfViews.AddResult.SecondDayThirdExercise.Content = exer.ElementAt(num).Second_Day;
        //            WindowOfViews.AddResult.ThirdDayThirdExercise.Content = exer.ElementAt(num).Third_Day;
        //            num++;
        //            WindowOfViews.AddResult.FirstDayFourthExercise.Content = exer.ElementAt(num).First_Day;
        //            WindowOfViews.AddResult.SecondDayFourthExercise.Content = exer.ElementAt(num).Second_Day;
        //            WindowOfViews.AddResult.ThirdDayFourthExercise.Content = exer.ElementAt(num).Third_Day;
        //            num++;
        //            WindowOfViews.AddResult.FirstDayFifthExercise.Content = exer.ElementAt(num).First_Day;
        //            WindowOfViews.AddResult.SecondDayFifthExercise.Content = exer.ElementAt(num).Second_Day;
        //            WindowOfViews.AddResult.ThirdDayFifthExercise.Content = exer.ElementAt(num).Third_Day;
        //            num++;
        //            WindowOfViews.AddResult.FirstDaySixthExercise.Content = exer.ElementAt(num).First_Day;
        //            WindowOfViews.AddResult.SecondDaySixthExercise.Content = exer.ElementAt(num).Second_Day;
        //            WindowOfViews.AddResult.ThirdDaySixthExercise.Content = exer.ElementAt(num).Third_Day;
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //        }
        //    }
        //}
    }
}
