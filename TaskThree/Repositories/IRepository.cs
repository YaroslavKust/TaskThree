using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskThree.Models;

namespace TaskThree.Repositories
{
    interface IRepository
    {
        /// <summary>
        /// This method compare records with filter object and return rigth records
        /// </summary>
        IEnumerable<Record> GetRecordsWithFilter(Record filterRec);
        Task AddRecordsAsync(IEnumerable<Record> records);
    }
}
