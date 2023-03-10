using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace TODOGO
{
    public static class Saver
    {
        static private string filePath = Environment.ExpandEnvironmentVariables("%ProgramW6432%")+"/TODOGO/";
        static public string Task = "tasks.json";
        static public string Setting = "setting.json";


        public static void SaveToJsonFile<T>(T data, string pr)
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            if(!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            File.WriteAllText(filePath+pr, json);
        }


        public static List<TaskViewModel> ReadFromJsonFile()
        {
            if (!File.Exists(filePath + Task))
                SaveToJsonFile<List<TaskViewModel>>(new List<TaskViewModel> { new TaskViewModel() }, Task);
            string json = File.ReadAllText(filePath+Task);
            return JsonConvert.DeserializeObject<List<TaskViewModel>>(json);
        }

        public static SettingViewModel ReadFromJsonFileSetting()
        {
            if (!File.Exists(filePath))
                SaveToJsonFile<SettingViewModel>(new SettingViewModel() , Setting);
            string json = File.ReadAllText(filePath+Setting);
            return JsonConvert.DeserializeObject<SettingViewModel>(json);
        }


    }
}