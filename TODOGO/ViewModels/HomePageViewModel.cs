using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TODOGO
{
    public class HomePageViewModel : ViewModelBase
    {
        public List<TaskViewModel> Tasks { get; set; }


        public HomePageViewModel()
        {
            Tasks = SavesMenager.ReadFromJsonFile<List<TaskViewModel>>();
        }
    }
}
