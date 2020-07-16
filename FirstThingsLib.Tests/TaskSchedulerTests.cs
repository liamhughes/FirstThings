using System;
using System.Collections.Generic;
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

        [TestMethod]
        public void TaskSchedulerOnlyReturnsTasksThatHaveNotBeenCompletedOrArchived()
        {
            var scheduler = new FirstThingsLib.TaskScheduler();

            var task1 = new FirstThingsLib.Task { Order = -30, Status = (int)TaskStatus.Normal };
            var task2 = new FirstThingsLib.Task { Order = -20, Status = (int)TaskStatus.Completed };
            var task3 = new FirstThingsLib.Task { Order = -10, Status = (int)TaskStatus.Archived };

            var tasks = new List<FirstThingsLib.Task>{ task1, task2, task3 };

            var scheduleOptions = new FirstThingsLib.ScheduleOptions();

            var scheduledTasks = scheduler.ScheduleTasks(tasks, scheduleOptions);

            scheduledTasks.Should().BeEquivalentTo(new List<FirstThingsLib.Task> { task1 });
        }

        [TestMethod]
        public void TaskSchedulerReturnsTasksBasedOnDueDate()
        {
            var now = DateTime.UtcNow;

            var scheduler = new FirstThingsLib.TaskScheduler();

            var task1 = new FirstThingsLib.Task { Order = -30, Status = (int)TaskStatus.Normal, StartDate = now };
            var task2 = new FirstThingsLib.Task { Order = -20, Status = (int)TaskStatus.Normal, StartDate = now.AddMilliseconds(-1) };
            var task3 = new FirstThingsLib.Task { Order = -10, Status = (int)TaskStatus.Normal, StartDate = now.AddMilliseconds(1) };

            var tasks = new List<FirstThingsLib.Task>{ task1, task2, task3 };

            var scheduleOptions = new FirstThingsLib.ScheduleOptions
            {
                StartDate = now
            };

            var scheduledTasks = scheduler.ScheduleTasks(tasks, scheduleOptions);

            scheduledTasks.Should().BeEquivalentTo(new List<FirstThingsLib.Task> { task1, task2 });
        }

        [TestMethod]
        public void TaskSchedulerReturnsTasksWithoutADueDate()
        {
            var now = DateTime.UtcNow;

            var scheduler = new FirstThingsLib.TaskScheduler();

            var task1 = new FirstThingsLib.Task { Order = -30, Status = (int)TaskStatus.Normal };
            var task2 = new FirstThingsLib.Task { Order = -20, Status = (int)TaskStatus.Normal, StartDate = null };

            var tasks = new List<FirstThingsLib.Task>{ task1, task2 };

            var scheduleOptions = new FirstThingsLib.ScheduleOptions
            {
                StartDate = now
            };

            var scheduledTasks = scheduler.ScheduleTasks(tasks, scheduleOptions);

            scheduledTasks.Should().BeEquivalentTo(new List<FirstThingsLib.Task> { task1, task2 });
        }

        [TestMethod]
        public void TaskSchedulerReturnsTasksForASpecifiedList()
        {
            const string LIST_ONE = "ListOne";
            const string LIST_TWO = "ListTwo";

            var now = DateTime.UtcNow;

            var scheduler = new FirstThingsLib.TaskScheduler();

            var task1 = new FirstThingsLib.Task { Order = -30, Status = (int)TaskStatus.Normal, ListName = LIST_ONE };
            var task2 = new FirstThingsLib.Task { Order = -20, Status = (int)TaskStatus.Normal, ListName = LIST_TWO };

            var tasks = new List<FirstThingsLib.Task>{ task1, task2 };

            var scheduleOptions = new FirstThingsLib.ScheduleOptions
            {
                List = LIST_ONE
            };

            var scheduledTasks = scheduler.ScheduleTasks(tasks, scheduleOptions);

            scheduledTasks.Should().BeEquivalentTo(new List<FirstThingsLib.Task> { task1 });
        }

        [TestMethod]
        public void TaskSchedulerReturnsAllTasksIfAListIsNotSpecified()
        {
            const string LIST_ONE = "ListOne";
            const string LIST_TWO = "ListTwo";

            var now = DateTime.UtcNow;

            var scheduler = new FirstThingsLib.TaskScheduler();

            var task1 = new FirstThingsLib.Task { Order = -30, Status = (int)TaskStatus.Normal, ListName = LIST_ONE };
            var task2 = new FirstThingsLib.Task { Order = -20, Status = (int)TaskStatus.Normal, ListName = LIST_TWO };

            var tasks = new List<FirstThingsLib.Task>{ task1, task2 };

            var scheduleOptions = new FirstThingsLib.ScheduleOptions
            {
                List = null
            };

            var scheduledTasks = scheduler.ScheduleTasks(tasks, scheduleOptions);

            scheduledTasks.Should().BeEquivalentTo(new List<FirstThingsLib.Task> { task1, task2 });
        }
    }
}
