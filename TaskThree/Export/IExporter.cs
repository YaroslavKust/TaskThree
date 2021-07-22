using TaskThree.Models;

namespace TaskThree.Export
{
    interface IExporter
    {
        void Export(Record record);
    }
}
