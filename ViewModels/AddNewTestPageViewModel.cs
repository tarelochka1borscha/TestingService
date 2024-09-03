using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Windows;
using TestingService.Models;
using TestingService.Views;

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

        private RelayCommand _deleteQuestion;
        public RelayCommand DeleteQuestion
        {
            get
            {
                return _deleteQuestion ??
                  (_deleteQuestion = new RelayCommand(obj =>
                  {
                      Questions question = obj as Questions;
                      RemoveQuestion(question);
                  }));
            }
        }


        private RelayCommand _backPage;
        public RelayCommand BackPage
        {
            get
            {
                return _backPage ?? new RelayCommand(obj =>
                {
                    Back();
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

        private Questions _selectedQuestion;
        public Questions SelectedQuestion
        {
            get => _selectedQuestion;
            set
            {
                _selectedQuestion = value;
                NotifyPropertyChanged(nameof(SelectedQuestion));
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
                    return;
                }

                DatabaseConnection.connection.Tests.Add(_currentTest);
                DatabaseConnection.connection.SaveChanges();

                 foreach (Questions question in QuestionsList)
                {
                    question.QuestionTestId = _currentTest.TestId;
                    DatabaseConnection.connection.Questions.Add(question);
                }
                 DatabaseConnection.connection.SaveChanges();
                MessageBox.Show("Тест успешно добавлен");
                Back();
            }
            else
            {
                MessageBox.Show("Данные не сохранены");
            }
        }

        private bool CheckingData()
        {
            if ((TestTitle == null) || (QuestionsList.Count < 1))
            {
                MessageBox.Show("Не все поля заполнены!");
                return false;
            }
            foreach (Questions item in QuestionsList)
            {
                if ((item.QuestionTitle == null) || (item.QuestionAnswer == null))
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
        }

        private void RemoveQuestion(Questions question)
        {
            QuestionsList.Remove(question);
        }

        private void Back()
        {
            FrameClass.frame.Navigate(new MainPage());
        }
    }
}
