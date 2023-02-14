using DevExpress.Mvvm;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
using System.ComponentModel;

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


        public List<TaskViewModel> Tasks { get; set; }
        
        public ICommand SaveTasks
        {
            get
            {
                return new DelegateCommand<CancelEventArgs>((x) =>
                {
                    SavesMenager.SaveToJsonFile(Tasks);
                });

            }
        }


        public CalendarPageViewModel CalendarPage { get; set; } = new CalendarPageViewModel();


        public AppViewModel()
        {
            Tasks = SavesMenager.ReadFromJsonFile<List<TaskViewModel>>();
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
