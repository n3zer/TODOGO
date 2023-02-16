using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODOGO
{
    public class CalendarViewModel:ViewModelBase
    {
        public ObservableCollection<TaskViewModel> FilterTasks { get; set; }
        public bool ApplyFilter { get; set; }
        public DateTime SelectedDate { get; set; }

        public TaskViewModel SelectedTask { get; set; }


        public CalendarViewModel() 
        {
            SelectedDate= DateTime.Now.Date;
        }
    }
}
