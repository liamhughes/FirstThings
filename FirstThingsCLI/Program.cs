using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleTables;

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

            var scheduledTasks = taskScheduler.ScheduleTasks(
                tasks.ToList(),
                new FirstThingsLib.ScheduleOptions
                {
                    List = "Work",
                    StartDate = DateTime.Now
                }
            );

            OutputTasks(scheduledTasks.ToList());
        }

        static void OutputTasks(IList<FirstThingsLib.Task> tasks)
        {
            var table = new ConsoleTable(nameof(FirstThingsLib.Task.Title), nameof(FirstThingsLib.Task.Duration));

            foreach(var task in tasks)
                table.AddRow(task.Title, task.Duration);
            
            table.Write();
        }
    }
}
