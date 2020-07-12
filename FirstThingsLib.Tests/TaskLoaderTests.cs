using System.Collections.Generic;
using System.Linq;
using FirstThingsLib;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FirstThingsLib.Tests
{
    [TestClass]
    public class TaskLoaderTests
    {
        [TestMethod]
        public void TasksCanBeLoaded()
        {
            var loader = new FirstThingsLib.TaskLoader();

            var tasks = loader.LoadTasks();

            tasks.Should().BeAssignableTo<IEnumerable<FirstThingsLib.Task>>();
        }
    }
}
