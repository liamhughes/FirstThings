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
    public class FileTaskLoaderTests
    {
        [TestMethod]
        public void TasksCanBeLoaded()
        {
            const string FILE_PATH = @"tasks.csv";

            var testTasks = CreateTestTasks();

            var fileSystem = CreateMockFileSystem(FILE_PATH, testTasks);

            var loader = new FirstThingsLib.FileTaskLoader(fileSystem, FILE_PATH);

            var tasks = loader.LoadTasks();

            tasks.Should().BeAssignableTo<IEnumerable<FirstThingsLib.Task>>();

            tasks.Count().Should().Be(testTasks.Count());

            tasks.Should().BeEquivalentTo(testTasks);
        }

        private IEnumerable<Task> CreateTestTasks()
            => new List<Task>
            {
                new Task 
                { 
                    Title = "Task 1", 
                    ListName = "Personal",
                    Order = -7493990364060050000,
                    StartDate = new DateTime(2020, 1, 1),
                    Status = 0, 
                },
                new Task 
                { 
                    Title = "Task 2", 
                    ListName = "Personal", 
                    Order = -1152922397960040000,
                    StartDate = new DateTime(2020, 2, 2),
                    Status = 1,
                },
            };

        private static MockFileSystem CreateMockFileSystem(string FILE_PATH, IEnumerable<Task> testTasks)
        {   
            return new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { FILE_PATH, new MockFileData(CsvContentFromTasks(testTasks)) }
            });
        }

        private static string CsvContentFromTasks(IEnumerable<Task> tasks)
        {
            var stringBuilder = new StringBuilder();
            
            var stringWriter = new StringWriter(stringBuilder);

            using(var csv = new CsvWriter(stringWriter, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(tasks);
            }

            var csvContent = stringBuilder.ToString();

            return csvContent;
        }
    }
}
