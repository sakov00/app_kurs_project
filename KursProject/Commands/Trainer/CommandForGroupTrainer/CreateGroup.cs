using KursProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KursProject.Commands.CommandForGroupTrainer
{
    class CreateGroup : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public CreateGroup(Action<object> execute, Func<object, bool> canExecute = null)
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
                try
                {
                    Random value = new Random();
                    int number = value.Next(0, 100);
                    var groups=db.Group.ToList();
                    Found: foreach(var g in groups)
                    {
                        if (g.Number_Group == number)
                        {
                            number = value.Next(0, 100000);
                            goto Found;
                        }
                    }
                    Trainer trainer = WorkTrainerWindowViewModel.trainer;
                    Group group = new Group();
                    group.Number_Group = number;
                    group.Id_Trainer = trainer.id;
                    db.Group.Add(group);
                    db.SaveChanges();
                    MessageBox.Show("группа создана, но в ней никто не участвует добавьте клиентов");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
