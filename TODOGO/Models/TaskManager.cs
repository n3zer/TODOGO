using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

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

        public static ObservableCollection<TaskViewModel> ClearEmptyTask(ObservableCollection<TaskViewModel> tasks)
        {
            ObservableCollection<TaskViewModel> result = new ObservableCollection<TaskViewModel>();
            if (tasks != null)
                foreach (TaskViewModel item in tasks)
                    if (!string.IsNullOrWhiteSpace(item.Name))
                    {
                        result.Add(item);
                    }
            return result;
        }


        public static void AddTask(this ObservableCollection<TaskViewModel> tasks, out TaskViewModel selectedTask)
        {
            TaskViewModel task = new TaskViewModel() { Day = DateTime.Now.Date };
            tasks.Insert(0, task);
            selectedTask = task;
        }

        public static int[] GetCountFinishedTasks(ObservableCollection<TaskViewModel> tasks)
        {
            if (tasks == null)
                return new int[] {0,0};

            Dictionary<DateTime, int> finishedDate = new Dictionary<DateTime, int>();

            foreach (TaskViewModel task in tasks)
            {
                if (task.IsComplete)
                {
                    if (!finishedDate.ContainsKey(task.Day))
                        finishedDate.Add(task.Day, 1);
                    else
                        finishedDate[task.Day]++;
                }
               
            }
            return finishedDate.Values.Reverse().ToArray();
        }

        public static DateTime[] GetDateWithFinishedTasks(ObservableCollection<TaskViewModel> tasks)
        {
            if (tasks == null)
                return new DateTime[] { DateTime.Now};
            List<DateTime> result = new List<DateTime>();
            foreach (TaskViewModel task in tasks)
                if (task.IsComplete && !result.Contains(task.Day))
                    result.Insert(0,task.Day);



            return result.OrderByDescending(d => d).Reverse().ToArray();
        }


        public static int[] GetTasksCount(ObservableCollection<TaskViewModel> tasks)
        {
            List<int> result = new List<int>();
            foreach (int item in GetCountFinishedTasks(tasks))
                result.Add(item);

            return result.ToArray();
        }
    }
}
