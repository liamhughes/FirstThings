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

        [TestMethod]
        public void TaskSchedulerReturnsTasksOrderedByOrderProperty()
        {
            var scheduler = new FirstThingsLib.TaskScheduler();

            var task1 = new FirstThingsLib.Task { Order = -30 };
            var task2 = new FirstThingsLib.Task { Order = -20 };
            var task3 = new FirstThingsLib.Task { Order = -10 };

            var tasks = new List<FirstThingsLib.Task>{ task1, task3, task2 };

            var scheduleOptions = new FirstThingsLib.ScheduleOptions();

            var scheduledTasks = scheduler.ScheduleTasks(tasks, scheduleOptions);

            scheduledTasks.Should().BeInAscendingOrder(t => t.Order);
        }
    }
}
