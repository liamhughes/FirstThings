using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using System.Text;
using CsvHelper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FirstThingsLib.Tests
{
    [TestClass]
    public class TaskSchedulerTests
    {
        [TestMethod]
        public void TaskSchedulerCanBeConstructed()
        {
            var scheduler = new FirstThingsLib.TaskScheduler();
        }

        [TestMethod]
        public void TaskSchedulerCanScheduleTasksBasedOnScheduleOptions()
        {
            var scheduler = new FirstThingsLib.TaskScheduler();
            
            var tasks = new List<FirstThingsLib.Task>();
            
            var scheduleOptions = new FirstThingsLib.ScheduleOptions();

            var scheduledTasks = scheduler.ScheduleTasks(tasks, scheduleOptions);
        }

        
    }
}
