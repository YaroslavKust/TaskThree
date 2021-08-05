using System.Collections.Generic;
using System.Threading.Tasks;
using TaskThree.Models;

namespace TaskThree.Export
{
    interface IExporter
    {
        Task ExportAsync(List<Record> records, string fileName);
    }
}
