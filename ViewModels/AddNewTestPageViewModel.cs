using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        public string TestTitle { get; set; }
        public string TestDescription { get; set; }

        public string QuestionTitle { get; set; }
        public string QuestionDescription { get; set; }

        public ObservableCollection<Questions> QuestionsList { get; set; }


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

        private RelayCommand _saveTest;
        public RelayCommand SaveTest
        {
            get
            {
                return _saveTest ?? new RelayCommand(obj =>
                {
                    UpdateData();
                });
            }
        }

        private Tests _currentTest;

        public AddNewTestPageViewModel()
        {
            _currentTest = new Tests();
            QuestionsList = new ObservableCollection<Questions>();
            DatabaseConnection.connection = new DatabaseEntities();
        }

        private void UpdateData()
        {
            if (CheckingData())
            {
                _currentTest.TestTitle = TestTitle;
                try
                {
                    _currentTest.TestDescription = TestDescription;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Неопознанная ошибка");
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Данные не сохранены");
            }
        }

        private bool CheckingData()
        {
            if ((TestTitle == null) /*|| (QuestionsList.Count <= 0)*/)
            {
                MessageBox.Show("Не все поля заполнены!");
                return false;
            }
            foreach (Questions item in QuestionsList)
            {
                if ((item.QuestionTitle == null) || item.QuestionTypes == null)
                {
                    MessageBox.Show("Не все поля заполнены!");
                    return false;
                }
            }
            return true;
        }

        private void NewQuestion()
        {
            QuestionsList.Add(new Questions());
            MessageBox.Show("Добавлен новый вопрос");
        }
    }
}
