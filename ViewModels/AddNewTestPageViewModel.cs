using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingService.Models;

namespace TestingService.ViewModels
{
    public class AddNewTestPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private RelayCommand _addNewQuestion;
        public RelayCommand AddNewQuestion
        {
            get
            {
                return _addNewQuestion ?? new RelayCommand(obj =>
                {
                    NewQuestion();
                });
            }
        }


        public AddNewTestPageViewModel()
        {
            DatabaseConnection.connection = new DatabaseEntities();
        }

        private void NewQuestion()
        {

        }
    }
}
