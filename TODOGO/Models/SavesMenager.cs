using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace TODOGO
{
    public static class SavesMenager
    {
        static private string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "viewModels.json");

        public static void SaveToJsonFile<T>(T data)
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }


        public static T ReadFromJsonFile<T>()
        {
            if (!File.Exists(filePath))
                SaveToJsonFile<List<TaskViewModel>>(new List<TaskViewModel> { new TaskViewModel() });
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }

    }
}