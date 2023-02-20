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
using System.Windows.Documents;
using System.Globalization;
using DevExpress.Mvvm.Native;

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


        // pages vm
        public HomeViewModel HomeVM { get; set; }
        public CalendarViewModel CalendarVM { get; set; }


        
        

        // для выноса за предела класса 
        public ICommand DisableFilterTasksDate
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (!CalendarVM.ApplyFilter)
                        CalendarVM.FilterTasks = Tasks;
                    else
                        FilterTasksDate.Execute(CalendarVM.SelectedDate.Date);
                });

            }
        }
        public ICommand FilterTasksDate
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    CalendarVM.ApplyFilter = true;
                    CalendarVM.FilterTasks = TaskManager.GetTasksByDate(CalendarVM.SelectedDate.Date, Tasks);
                });

            }
        }

        public ICommand CheckEmptyTasks
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Tasks.ClearEmptyTask(CalendarVM.SelectedTask);
                });

            }
        }
        

        public ICommand RemoveTask
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (CalendarVM.SelectedTask != null)
                        Tasks.Remove(CalendarVM.SelectedTask);
                });

            }
        }

        public ICommand AddTask
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    TaskViewModel task = new TaskViewModel() { Date = DateTime.Now.Date };
                    Tasks.Insert(0, task);
                    CalendarVM.SelectedTask = task;
                    Tasks.ClearEmptyTask(CalendarVM.SelectedTask);
                    


                });

            }
        }

        public ICommand SaveTasks
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    SavesMenager.SaveToJsonFile(TaskManager.ClearEmptyTask(Tasks));
                });

            }
        }




        public AppViewModel()
        {
            Tasks = SavesMenager.ReadFromJsonFile<ObservableCollection<TaskViewModel>>();
            Tasks = Tasks.OrderBy(x => x.Date).Reverse().ToObservableCollection();
            Tasks = TaskManager.SetUncompletedTasks(Tasks);


            HomeVM = new HomeViewModel(Tasks);
            CalendarVM = new CalendarViewModel();

            HomeVM.Tasks = Tasks;
            
            CalendarVM.FilterTasks = Tasks;
            CalendarVM.SelectedDate = DateTime.Now;

            

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
