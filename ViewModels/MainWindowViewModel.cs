using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestingService.Models;

namespace TestingService.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public RoutedCommand Command { get; set; } = new RoutedCommand();

        public MainWindowViewModel()
        {
            DatabaseConnection.connection = new DatabaseEntities();
        }

        public void CreateNewTest()
        {

        }
    }
}
