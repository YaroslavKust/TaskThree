using System.Collections.Generic;
using TaskThree.Models;

namespace TaskThree.Services
{
    interface IFileService
    {
        List<Record> ReadAll(string fileName);
    }
}
