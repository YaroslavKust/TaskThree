using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskThree.Models;

namespace TaskThree.Services
{
    class CsvFileService: IFileService
    {
        public async Task<List<Record>> ReadAllAsync(string fileName)
        {
            var records = new List<Record>();
            var reader = new StreamReader(fileName, Encoding.UTF8);

            int propertiesCount = typeof(Record).GetProperties().Count(p => p.CanWrite);

            string line;

            while ((line = await reader.ReadLineAsync()) != null)
            {
                string[] data = line.Split(';');

                if (!
                    (data.Length == propertiesCount - 1
                    &&
                    DateTime.TryParse(data[0], out _)
                    &&
                    Record.CheckNameFormat(data[1])
                    &&
                    Record.CheckNameFormat(data[2])
                    &&
                    Record.CheckNameFormat(data[3])
                    &&
                    Record.CheckPlaceFormat(data[4])
                    &&
                    Record.CheckPlaceFormat(data[5]))
                )
                    throw new InvalidDataException("Неверный формат данных");

                Record rec = new()
                {
                    Date = DateTime.Parse(data[0]),
                    FirstName = data[1],
                    LastName = data[2],
                    SurName = data[3],
                    City = data[4],
                    Country = data[5]
                };
                records.Add(rec);
            }

            return records;
        }
    }
}
