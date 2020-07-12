using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using CsvHelper;

namespace FirstThingsLib
{
    public class FileTaskLoader
    {
        public FileTaskLoader(IFileSystem fileSystem, string filePath)
        {
            FileSystem = fileSystem;
            FilePath = filePath;
        }

        public IFileSystem FileSystem { get; }
        public string FilePath { get; }

        public IEnumerable<Task> LoadTasks()
        {
            var tasks = default(IEnumerable<Task>);

            var csvContent = FileSystem.File.ReadAllText(FilePath);

            using (var stream = FileSystem.FileStream.Create(FilePath, FileMode.Open))
            using (var streamReader = new StreamReader(stream))
            using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
            {    
                tasks = csvReader.GetRecords<Task>().ToList();
            }
            
            return tasks;
        }
    }
}