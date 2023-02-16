using DevExpress.Mvvm;
using System;
using System.Collections.ObjectModel;


namespace TODOGO
{
    public class HomeViewModel:ViewModelBase
    {
        public ObservableCollection<TaskViewModel> TodayTasks { get; set; }

        public HomeViewModel(ObservableCollection<TaskViewModel> tasks) 
        {
            TodayTasks= TaskManager.GetTasksByDate(DateTime.Now.Date, tasks);
        }
    }
}
