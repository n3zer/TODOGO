using SQLite;
using System.Collections.Generic;
using System.Linq;


namespace TODOGO
{
    public class DatabaseManager
    {
        private readonly SQLiteConnection _database;

        public DatabaseManager()
        {
            _database = new SQLiteConnection("Data Source=viewmodels.db;Version=3;");
            _database.CreateTable<TaskViewModel>();
        }

        public void SaveViewModels(List<TaskViewModel> viewModels)
        {
            foreach (var viewModel in viewModels)
            {
                _database.Insert(viewModel);
            }
        }

        public List<TaskViewModel> LoadViewModels()
        {
            return _database.Table<TaskViewModel>().ToList();
        }
    }
}
