using System;

namespace TODOGO
{
    public class TaskViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public DateOnly Day { get; set; }
        public TimeOnly Time { get; set; }

        public bool IsComplete { get; set; }

    }
}
