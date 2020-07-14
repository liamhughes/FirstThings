using System;
using System.Collections.Generic;
using System.Linq;

namespace FirstThingsLib
{
    public class TaskScheduler
    {
        public TaskScheduler()
        {
        }

        public List<Task> ScheduleTasks(List<Task> tasks, ScheduleOptions scheduleOptions)
        {
            return tasks.OrderBy(t => t.Order).ToList();
        }
    }
}