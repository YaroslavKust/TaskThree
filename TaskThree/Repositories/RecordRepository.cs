using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskThree.Models;

namespace TaskThree.Repositories
{
    class RecordRepository: IRepository
    {
        private TaskThreeContext _db;

        public RecordRepository()
        {
            _db = new TaskThreeContext();
        }

        public IEnumerable<Record> GetRecordsWithFilter(Record rec)
        {
            IEnumerable<Record> records = _db.Records.Where(r =>
                (r.Date.DayOfYear == rec.Date.DayOfYear) && (r.Date.Year == rec.Date.Year)
                &&
                (String.IsNullOrWhiteSpace(rec.FirstName) || r.FirstName == rec.FirstName)
                &&
                (String.IsNullOrWhiteSpace(rec.LastName) || r.LastName == rec.LastName)
                &&
                (String.IsNullOrWhiteSpace(rec.SurName) || r.SurName == rec.SurName)
                &&
                (String.IsNullOrWhiteSpace(rec.City) || r.City == rec.City)
                &&
                (String.IsNullOrWhiteSpace(rec.Country) || r.Country == rec.Country)
            );

            return records;
        }

        public async Task AddRecordsAsync(IEnumerable<Record> records)
        {
            await _db.AddRangeAsync(records);
            await _db.SaveChangesAsync();
        }
    }
}
