using System;
using System.Collections.Generic;
using System.Linq;
using TaskThree.Models;

namespace TaskThree.Repositories
{
    class SQLServerRepository: IRepository
    {
        private TaskThreeContext _db;

        public SQLServerRepository()
        {
            _db = new TaskThreeContext();
        }

        public IEnumerable<Record> GetRecordsWithFilter(Record rec)
        {
            IEnumerable<Record> Records = _db.Records.Where(r =>
                rec.Date == DateTime.MinValue || r.Date == rec.Date
                    &&
                    String.IsNullOrWhiteSpace(rec.FirstName) || r.FirstName == rec.FirstName
                    &&
                    String.IsNullOrWhiteSpace(rec.LastName) || r.LastName == rec.LastName
                    &&
                    String.IsNullOrWhiteSpace(rec.SurName) || r.SurName == rec.SurName
                    &&
                    String.IsNullOrWhiteSpace(rec.City) || r.City == rec.City
                    &&
                    String.IsNullOrWhiteSpace(rec.Country) || r.Country == rec.Country
            );

            return Records;
        }

        public async void AddRecordsAsync(IEnumerable<Record> records)
        {
            await _db.AddRangeAsync(records);
            _db.SaveChanges();
        }
    }
}
