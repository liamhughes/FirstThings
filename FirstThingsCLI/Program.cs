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

            var tasksToOutput = tasks
                                    .Where(t => t.ListName == "Personal - New")
                                    .Where(t => t.Status == 0)
                                    .Where(t => t.StartDate <= DateTime.Now);

            foreach(var task in tasksToOutput.OrderBy(t => t.Order))
            {
                Console.WriteLine($"{task.Title}");
            }
        }
    }
}
