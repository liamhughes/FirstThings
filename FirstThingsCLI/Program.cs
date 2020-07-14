using System;
using System.Linq;

namespace FirstThingsCLI
{
    class Program
    {
        const string FILE_PATH = "/Users/liam/Downloads/TickTick.csv";

        static void Main(string[] args)
        {
            var fileSystem = new System.IO.Abstractions.FileSystem();

            var taskLoader = new FirstThingsLib.FileTaskLoader(fileSystem, FILE_PATH);

            var tasks = taskLoader.LoadTasks();

            var taskScheduler = new FirstThingsLib.TaskScheduler();

            var scheduledTasks = taskScheduler.ScheduleTasks(tasks.ToList(), new FirstThingsLib.ScheduleOptions());

            var tasksToOutput = scheduledTasks
                                    .Where(t => t.ListName == "Personal - New")
                                    .Where(t => t.Status == 0)
                                    .Where(t => t.StartDate <= DateTime.Now);

            foreach(var task in tasksToOutput)
            {
                Console.WriteLine($"{task.Title} - {task.Duration?.TotalMinutes.ToString() ?? "??"} minutes");
            }
        }
    }
}
