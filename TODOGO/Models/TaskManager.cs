using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODOGO
{
    public static class TaskManager
    {
        public static ObservableCollection<TaskViewModel> GetTasksByDate(DateTime date, ObservableCollection<TaskViewModel> tasks) 
        {
            var oc = new ObservableCollection<TaskViewModel>();
            foreach (var item in tasks)
                if (item.Day == date)
                    oc.Add(item);
            return oc;
        }

        public static void ClearEmptyTask(this ObservableCollection<TaskViewModel> tasks, TaskViewModel selectedTask)
        {
            if (tasks != null)
                foreach (TaskViewModel item in tasks)
                    if (string.IsNullOrWhiteSpace(item.Name)
                    && item != selectedTask)
                    {
                        tasks.Remove(item);
                        return;
                    }
        }

        public static void AddTask(this ObservableCollection<TaskViewModel> tasks, out TaskViewModel selectedTask)
        {
            TaskViewModel task = new TaskViewModel() { Day = DateTime.Now.Date };
            tasks.Insert(0, task);
            selectedTask = task;
        }
    }
}
