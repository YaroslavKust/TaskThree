using System.Collections.Generic;
using System.Threading.Tasks;
using TaskThree.Models;

namespace TaskThree.Services
{
    interface IFileService
    {
        Task<List<Record>> ReadAllAsync(string fileName);
    }
}
