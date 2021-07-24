using System.Collections.Generic;
using TaskThree.Models;

namespace TaskThree.Export
{
    interface IExporter
    {
        void Export(ICollection<Record> records);
    }
}
