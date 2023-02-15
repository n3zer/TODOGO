using DevExpress.Mvvm;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
using System.ComponentModel;
using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace TODOGO
{
    public class AppViewModel : BindableBase
    {
        private Dictionary<string, Page> _appPages;
        public Page CurrentPage { get; set; }

        public ICommand ChangePage
        {
            get
            {
                return new DelegateCommand<string>((x) =>
                {
                    if (_appPages.Keys.Contains(x))
                    {
                        CurrentPage = _appPages[x];

                    }
                });

            }
        }

       

        public ObservableCollection<TaskViewModel> Tasks { get; set; }
        public TaskViewModel SelectedTask { get; set; }


        public ICommand RemoveTask
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (SelectedTask != null)
                        Tasks.Remove(SelectedTask);
                });

            }
        }

        public ICommand AddTask
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    TaskViewModel task = new TaskViewModel() {Day=DateTime.Now};
                    Tasks.Insert(0, task);
                    SelectedTask = task;
                });

            }
        }

        public ICommand SaveTasks
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    SavesMenager.SaveToJsonFile(Tasks);
                });

            }
        }




        public AppViewModel()
        {
            Tasks = SavesMenager.ReadFromJsonFile<ObservableCollection<TaskViewModel>>();
            _appPages = new Dictionary<string, Page>
            {
                {"Home",  new HomePage(this)},
                {"Settings",  new SettingPage()},
                {"Tasks",  new TasksPage()},
                {"Calendar",  new CalendarPage(this)}
            };
            CurrentPage = _appPages["Home"];
        }
        
    }
}
