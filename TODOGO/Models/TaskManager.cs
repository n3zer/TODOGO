using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace TODOGO
{
    public static class TaskManager
    {
        public static ObservableCollection<TaskViewModel> GetTasksByDate(DateTime date, ObservableCollection<TaskViewModel> tasks)
        {
            var oc = new ObservableCollection<TaskViewModel>();
            foreach (var item in tasks)
                if (item.Date == date)
                    oc.Add(item);
            return oc;
        }

        public static void ClearEmptyTask(this ObservableCollection<TaskViewModel> tasks, TaskViewModel selectedTask)
        {
            if (tasks != null)
                foreach (TaskViewModel item in tasks)
                {
                    if (string.IsNullOrWhiteSpace(item.Name)
                        && item != selectedTask)
                    {
                        tasks.Remove(item);
                        return;
                    }
                    if (item != selectedTask)
                    {
                        item.CanNotified = true;
                        return;
                    }
                    else
                    {
                        item.CanNotified = false;
                    }
                }
                    

        }

        public static ObservableCollection<TaskViewModel> ClearEmptyTask(this ObservableCollection<TaskViewModel> tasks)
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
            TaskViewModel task = new TaskViewModel() { Date = DateTime.Now.Date };
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
                    if (!finishedDate.ContainsKey(task.Date))
                        finishedDate.Add(task.Date, 1);
                    else
                        finishedDate[task.Date]++;
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
                if (task.IsComplete && !result.Contains(task.Date))
                    result.Insert(0,task.Date);


            return result.OrderByDescending(d => d).Reverse().ToArray();
        }


        public static int[] GetTasksCount(ObservableCollection<TaskViewModel> tasks)
        {
            List<int> result = new List<int>();
            for (int i = 1; i <= GetCountFinishedTasks(tasks).Max(); i++)
                result.Add(i);

            return result.ToArray();
        }

        public static ObservableCollection<TaskViewModel> SetUncompletedTasks(ObservableCollection<TaskViewModel> tasks)
        {
            foreach (TaskViewModel task in tasks)
            {
                if (task.TaskType == TaskTypes.EveryDay && task.CompletedDate.Date != DateTime.Now.Date)
                {
                    task.IsComplete = false;
                    task.IsNotified = false;
                }
                else if (task.TaskType == TaskTypes.SelectedDay && task.CompletedDate.Date != DateTime.Now.Date && task.DayOfWeeks[DateTime.Now.DayOfWeek])
                {
                    task.IsComplete = false;
                    task.IsNotified= false;
                }
                    
            }
                

            return tasks;
        }

        public static void Notification(ObservableCollection<TaskViewModel> tasks)
        {
            if (tasks != null)
            {
                foreach (TaskViewModel task in tasks)
                {
                    if (task.Time.TimeOfDay <= DateTime.Now.TimeOfDay &&
                        !string.IsNullOrWhiteSpace(task.Name)
                        && !task.IsComplete && task.CompletedDate.Date != DateTime.Now.Date
                        && !task.IsNotified && task.Name.Length > 2
                        && task.CanNotified)
                    {
                        _ = TelegramBot.Bot.SendMessageAsync(task);
                        task.IsNotified = true;

                    }
                }
            }
        }
        


    }
}
