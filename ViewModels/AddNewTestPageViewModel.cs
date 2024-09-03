﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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

        private QuestionTypes _selectedQuestion;
        public QuestionTypes SelectedQuestion
        {
            get => _selectedQuestion;
            set
            {
                if (_selectedQuestion != value)
                {
                    _selectedQuestion = value;
                    NotifyPropertyChanged(nameof(SelectedQuestion));
                }
            }
        }

        public ObservableCollection<Questions> QuestionsList { get; set; }

        public ObservableCollection<QuestionTypes> QuestionTypesList { get; set; }

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
            List<QuestionTypes> a = DatabaseConnection.connection.QuestionTypes.ToList();
            QuestionTypesList = new ObservableCollection<QuestionTypes>(a);
            QuestionTypesList.Add(new QuestionTypes { QuestionTypeId = 5, QuestionTypeTitle = "ТЕСТ" });

            //QuestionTypes b = new QuestionTypes { QuestionTypeTitle = "Выбор одного варианта ответа" };
            //DatabaseConnection.connection.QuestionTypes.Add(b);
            //DatabaseConnection.connection.SaveChanges();
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
                foreach (var item in QuestionsList)
                {
                    //item.QuestionTypeId = SelectedQuestionType;
                    MessageBox.Show($"{item.QuestionTitle} {item.QuestionTypeId}");

                    MessageBox.Show("" + SelectedQuestion.QuestionTypeId);
                }

                
            }
            else
            {
                MessageBox.Show("Данные не сохранены");
            }
        }

        private bool CheckingData()
        {
            if ((TestTitle == null) || (QuestionsList.Count <= 0))
            {
                MessageBox.Show("Не все поля заполнены!");
                return false;
            }
            foreach (Questions item in QuestionsList)
            {
                if ((item.QuestionTitle == null) /*|| item.QuestionTypeId == 0*/)
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
    }
}
