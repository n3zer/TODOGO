using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace TODOGO
{
    public class TickTask
    {
        private DispatcherTimer _dispatcherTimer;

        public ObservableCollection<TaskViewModel> Tasks;

        private void OnTick(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                TaskManager.Notification(Tasks);
            });
        }
        

        public TickTask()
        {
            _dispatcherTimer = new();
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            _dispatcherTimer.Tick += new EventHandler(OnTick);
            _dispatcherTimer.Start();
        }

        public void StopTicks() => _dispatcherTimer.Stop();
        public void StartTicks() => _dispatcherTimer.Start();

    }
}
