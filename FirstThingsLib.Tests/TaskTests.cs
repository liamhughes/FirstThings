using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FirstThingsLib.Tests
{
    [TestClass]
    public class TaskTests
    {
        [TestMethod]
        public void ASingleTagCanBeSetUsingTagString()
        {
            var task = new FirstThingsLib.Task();

            task.TagString = "growth";

            task.Tags.Should().BeEquivalentTo(new List<string> { "growth" });
        }
        
        [TestMethod]
        public void MultipleTagsCanBeSetUsingTagString()
        {
            var task = new FirstThingsLib.Task();

            task.TagString = "growth, 5_minutes";

            task.Tags.Should().BeEquivalentTo(new List<string> { "5_minutes", "growth" });
        }

        [TestMethod]
        public void ADurationTagIsInterprettedAsTheTaskDuration()
        {
            var task = new FirstThingsLib.Task();

            task.TagString = "growth, 5_minutes";

            task.Duration.Should().Be(TimeSpan.FromMinutes(5));
        }

        [TestMethod]
        public void ATaskWithoutADurationTagHasANullDuration()
        {
            var task = new FirstThingsLib.Task();

            task.TagString = "growth";

            task.Duration.Should().Be(null);
        }

        [TestMethod]
        public void ATaskWithMultipleDurationTagsThrowsAnException()
        {
            var task = new FirstThingsLib.Task();

            task.TagString = "5_minutes, 10_minutes";

            Action action = () => {
                var foo = task.Duration;  
            };

            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void SettingDurationOnATaskWithoutADurationUpdatesTags()
        {
            var task = new FirstThingsLib.Task();

            task.Duration = TimeSpan.FromMinutes(5);

            task.Duration.Should().Be(TimeSpan.FromMinutes(5));
            task.Tags.Should().Contain("5_minutes");
            task.TagString.Should().Be("5_minutes");
        }

        [TestMethod]
        public void SettingDurationOnATaskWithADurationUpdatesTags()
        {
            var task = new FirstThingsLib.Task
            {
                TagString = "growth, 5_minutes"
            };

            task.Duration = TimeSpan.FromMinutes(10);

            task.Duration.Should().Be(TimeSpan.FromMinutes(10));
            task.Tags.Should().Contain("10_minutes");
            task.TagString.Should().Be("growth, 10_minutes");
        }
    }
}
