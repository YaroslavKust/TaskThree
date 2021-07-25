﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskThree.Models;

namespace TaskThree.Services
{
    class CSVFileService: IFileService
    {
        public List<Record> ReadAll(string fileName)
        {
            List<Record> records = new List<Record>();
            StreamReader reader = new StreamReader(fileName, Encoding.UTF8);

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] data = line.Split(';');
                Record rec = new Record()
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
