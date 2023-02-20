using DevExpress.Mvvm;
using Newtonsoft.Json;
using System;
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
        public DateTime Date { get; set; }

        [JsonProperty("Time")]
        public DateTime Time { get; set; }

        [JsonProperty("Dates")]
        public TaskTypes Dates { get; set; }

        [JsonProperty("TaskType")]
        public TaskTypes TaskType { get; set; }


        [JsonProperty("CompletedDate")]
        public DateTime CompletedDate { get; set; }

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
