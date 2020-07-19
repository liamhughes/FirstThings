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
            var result = tasks
                .Where(t => scheduleOptions.List == null || t.ListName == scheduleOptions.List)
                .Where(t => t.Status == (int)TaskStatus.Normal)
                .Where(t => t.StartDate == null || t.StartDate <= scheduleOptions.StartDate)
                .OrderBy(t => t.Order).ToList();

            var upToDate = scheduleOptions.StartDate;
            foreach(var task in result)
            {
                task.ScheduledStartDate = upToDate;
                upToDate += task.Duration.Value;
            }

            return result;
        }
    }
}