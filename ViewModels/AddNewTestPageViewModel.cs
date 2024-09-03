using System;
using System.Collections.Generic;
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

        private List<Questions> _questionsList;
        public List<Questions> QuestionsList
        {
            get => _questionsList;
            set
            {
                _questionsList = value;
                NotifyPropertyChanged("QuestionsList");
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
            QuestionsList = new List<Questions>();
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
                MessageBox.Show($"{_currentTest.TestId} {_currentTest.TestTitle} {_currentTest.TestDescription}");
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
            return true;
        }

        private void NewQuestion()
        {

        }
    }
}
