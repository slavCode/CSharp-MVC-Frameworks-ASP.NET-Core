namespace CarDealer.Services.Implementaions
{
    using Data;
    using Data.Models;
    using Models.Enums;
    using Models.Logs;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LogService : ILogService
    {
        private readonly CarDealerDbContext db;

        public LogService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<LogModel> All()
        {
            return this.db.Logs
                     .Select(l => new LogModel
                     {
                         User = this.db.Users.FirstOrDefault(u => u.Id == l.UserId).UserName,
                         Operation = (OperationType)Enum.Parse(typeof(OperationType), l.Operation),
                         Table = (ModyfiedTable)Enum.Parse(typeof(ModyfiedTable), l.ModyfiedTable),
                         Timestamp = l.Timestamp
                     })
                  .ToList();
        }

        public IEnumerable<LogModel> ByUsername(string username)
        {
            return this.db.Logs
                .Where(l => l.User.UserName.Contains(username))
                .Select(l => new LogModel
                {
                    User = this.db.Users.FirstOrDefault(u => u.Id == l.UserId).UserName,
                    Operation = (OperationType)Enum.Parse(typeof(OperationType), l.Operation),
                    Table = (ModyfiedTable)Enum.Parse(typeof(ModyfiedTable), l.ModyfiedTable),
                    Timestamp = l.Timestamp
                })
                .ToList();
        }

        public void Create(string userId, OperationType operation, ModyfiedTable table, DateTime dateTime)
        {
            var log = new Log
            {
                UserId = userId,
                Operation = operation.ToString(),
                ModyfiedTable = table.ToString(),
                Timestamp = dateTime
            };

            this.db.Add(log);
            this.db.SaveChanges();
        }

        public void DeleteAll()
        {
            var all = this.db.Set<Log>();

            this.db.Logs.RemoveRange(all);
            this.db.SaveChanges();
        }
    }
}
