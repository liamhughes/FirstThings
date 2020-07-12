using System;
using CsvHelper.Configuration.Attributes;

namespace FirstThingsLib
{
    public class Task
    {
        [Name("List Name")]
        public string ListName { get; set; }

        public double Order { get; set; }

        [Name("Start Date")]
        public DateTime? StartDate { get; set; }

        public int Status { get; set; }

        public string Title { get; set; }
    }
}