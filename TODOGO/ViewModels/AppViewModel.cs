using DevExpress.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm.Native;
using System.Linq;

namespace TODOGO
{
    public class AppViewModel : ViewModelBase
    {
        private Dictionary<string, Page> _appPages = new Dictionary<string, Page>
        {
            {"Home",  new HomePage()},
            {"Settings",  new SettingPage()}
        };
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

        public AppViewModel()
        {
            CurrentPage = _appPages["Settings"];
        }
    }
}
