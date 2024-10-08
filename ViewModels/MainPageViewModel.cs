﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TestingService.Models;
using TestingService.Views;

namespace TestingService.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private RelayCommand _openAddNewTest;
        public RelayCommand OpenAddNewTest
        {
            get
            {
                return _openAddNewTest ?? new RelayCommand(obj =>
                {
                    AddNewTest();
                });
            }
        }

        public MainPageViewModel()
        {
            DatabaseConnection.connection = new DatabaseEntities();
        }




        private void AddNewTest()
        {
            FrameClass.frame.Navigate(new AddNewTestPage());
            //AddNewTestWindow window = new AddNewTestWindow();
            //window.Owner = Application.Current.MainWindow;
            //window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            //window.ShowDialog();
        }
    }
}
