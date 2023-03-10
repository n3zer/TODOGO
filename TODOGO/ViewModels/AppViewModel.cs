using DevExpress.Mvvm;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Win32;
using DevExpress.Mvvm.Native;
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
                    CurrentPage = _appPages[x];
                });

            }
        }

       

        public ObservableCollection<TaskViewModel> Tasks { get; set; }
        public SettingViewModel Setting { get; set; }


        // pages vm
        public HomeViewModel HomeVM { get; set; }
        public CalendarViewModel CalendarVM { get; set; }
        


        
        

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


        public ICommand SetDay
        {
            get
            {
                return new DelegateCommand<DayOfWeek>((x) =>
                {
                    MessageBox.Show(CalendarVM.SelectedTask.DayOfWeeks[x].ToString());
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
                    if (Setting.AutoSaving)
                    {
                        Saver.SaveToJsonFile(Tasks.ClearEmptyTask(), Saver.Task);
                    }
                    Saver.SaveToJsonFile(Setting, Saver.Setting);
                });

            }
        }


        TickTask _tick;
        public AppViewModel()
        {
            Setting = Saver.ReadFromJsonFileSetting();
            Tasks = Saver.ReadFromJsonFile().ToObservableCollection();
            Tasks = Tasks.OrderBy(x => x.Date).Reverse().ToObservableCollection();
            Tasks = TaskManager.SetUncompletedTasks(Tasks);
            

            _tick = new()
            {
                Tasks = Tasks
            };

            HomeVM = new HomeViewModel(Tasks);
            CalendarVM = new CalendarViewModel();
            

            HomeVM.Tasks = Tasks;
            
            CalendarVM.FilterTasks = Tasks;
            CalendarVM.SelectedDate = DateTime.Now;

            TelegramBot.Bot = new(Setting.TelegramToken);

            _appPages = new Dictionary<string, Page>
            {
                {"Home",  new HomePage(this)},
                {"Settings",  new SettingPage(this)},
                {"Tasks",  new TasksPage()},
                {"Calendar",  new CalendarPage(this)}
            };
            CurrentPage = _appPages["Home"];
        }
        
    }
}
