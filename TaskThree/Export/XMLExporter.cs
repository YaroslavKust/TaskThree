﻿using System.Collections.Generic;
using System.Xml.Linq;
using TaskThree.Models;

namespace TaskThree.Export
{
    class XMLExporter: IExporter
    {
        public void Export(ICollection<Record> records) 
        {
            XDocument doc = new XDocument();
            XElement app = new XElement("Application");

            foreach(Record r in records)
            {
                XElement record = new XElement("Record");
                XAttribute id = new XAttribute("id", r.Id);

                XElement date = new XElement("Date", r.Date);
                XElement firstName = new XElement("FirstName", r.FirstName);
                XElement lastName = new XElement("LastName", r.LastName);
                XElement surName = new XElement("SurName", r.SurName);
                XElement city = new XElement("City", r.City);
                XElement country = new XElement("Country", r.Country);

                record.Add(id, date, firstName, lastName, surName, city, country);
                app.Add(record);
            }

            doc.Add(app);
            doc.Save("records.xml");
        }
    }
}
