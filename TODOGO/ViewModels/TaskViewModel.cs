using DevExpress.Mvvm;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace TODOGO
{

    public enum TaskTypes
    {
        [Description("Обычный")]
        Default,
        [Description("Каждый день")]
        EveryDay,
        [Description("Выбор дня")]
        SelectedDay
    }
    public class TaskViewModel : ViewModelBase
    {
        [JsonProperty("Name")]
        public string Name { get; set; } 

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Date")]
        public DateTime Date { get; set; } = DateTime.Now;

        [JsonProperty("Time")]
        public DateTime Time { get; set; } = DateTime.Now;

        [JsonProperty("Dates")]
        public TaskTypes Dates { get; set; }

        [JsonProperty("DayOfWeeks")]
        public Dictionary<DayOfWeek, bool> DayOfWeeks { get; set; } = new Dictionary<DayOfWeek, bool>()
        {
            {DayOfWeek.Monday, false},
            {DayOfWeek.Tuesday, false},
            {DayOfWeek.Wednesday, false},
            {DayOfWeek.Thursday, false},
            {DayOfWeek.Friday, false},
            {DayOfWeek.Saturday, false},
            {DayOfWeek.Sunday, false}

        };

        [JsonProperty("TaskType")]
        public TaskTypes TaskType { get; set; }


        [JsonProperty("CompletedDate")]
        public DateTime CompletedDate { get; set; }

        [JsonProperty("IsNotified")]
        public bool IsNotified { get; set; }

        [JsonProperty("CanNotified")]
        public bool CanNotified { get; set; }

        [JsonProperty("IsComplete")]
        public bool IsComplete { get; set; }


       public ICommand CompleteTask
        {
            get
            {
                return new DelegateCommand(()=>
                {
                    IsComplete= true;
                    CompletedDate= DateTime.Now.Date;

                });
            }
        }

    }
}
