using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace TODOGO
{
    public class HomePageViewModel : ViewModelBase
    {
        public ObservableCollection<TaskViewModel> Tasks { get; set; }

        public HomePageViewModel()
        {
            Tasks = new ObservableCollection<TaskViewModel>()
            {
                new TaskViewModel(){ Name = "Clear home", Description="For my mom", IsComplete=false},
                new TaskViewModel(){ Name = "Do homework", Description="For my mom", IsComplete=false},
                new TaskViewModel(){ Name = "Send email to Max", Description="For my mom", IsComplete=false},
            };

        }
    }
}
