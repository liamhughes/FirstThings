using System;
using System.Collections.Generic;
using System.Linq;
using CsvHelper.Configuration.Attributes;

namespace FirstThingsLib
{
    public class Task
    {
        [Name("List Name")]
        public string ListName { get; set; }

        public TimeSpan? Duration
        {
            get
            {
                var tag = Tags.SingleOrDefault(t => t.Contains("_minute"));

                if (tag == null)
                    return null;

                var minuteString = tag.Split(new List<string> { "_minute" }.ToArray(), StringSplitOptions.RemoveEmptyEntries)[0];

                return TimeSpan.FromMinutes(Convert.ToDouble(minuteString));
            }
        }

        public double Order { get; set; }

        [Name("Start Date")]
        public DateTime? StartDate { get; set; }

        public int Status { get; set; }

        [Name("Tags")]
        public string TagString
        { 
             get
             {
                 return String.Join(", ", Tags);
             } 
             set
             {
                Tags = value.Split(new List<string>{", "}.ToArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
             }
        }

        public List<string> Tags { get; set; } = new List<string>();

        public string Title { get; set; }
    }
}