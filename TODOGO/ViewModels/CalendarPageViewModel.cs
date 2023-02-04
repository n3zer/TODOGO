using DevExpress.Mvvm;
using System;

namespace TODOGO
{
    public class CalendarPageViewModel
    {
        public DateTime CurrentDate { get; set; }


        public CalendarPageViewModel()
        {
            CurrentDate = DateTime.Now.Date;
        }
    }
}
