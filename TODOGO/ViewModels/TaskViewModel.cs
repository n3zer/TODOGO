using DevExpress.Mvvm;
using Newtonsoft.Json;
using System;
using System.Windows.Input;

namespace TODOGO
{
    public class TaskViewModel : ViewModelBase
    {
        [JsonProperty("Name")]
        public string Name { get; set; }


        [JsonProperty("Description")]
        public string Description { get; set; }


        [JsonProperty("Day")]
        public DateTime Day { get; set; }


        [JsonProperty("Time")]
        public TimeOnly Time { get; set; }


        [JsonProperty("IsComplete")]
        public bool IsComplete { get; set; }


       public ICommand CompleteTask
        {
            get
            {
                return new DelegateCommand(()=>
                {
                    IsComplete= !IsComplete;
                });
            }
        }

    }
}
