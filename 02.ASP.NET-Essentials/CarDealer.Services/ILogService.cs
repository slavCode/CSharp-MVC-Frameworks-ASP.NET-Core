namespace CarDealer.Services
{
    using Models.Enums;
    using Models.Logs;
    using System;
    using System.Collections.Generic;

    public interface ILogService
    {
        IEnumerable<LogModel> All();

        IEnumerable<LogModel> ByUsername(string username);

        void Create(string user, OperationType operation, ModyfiedTable table, DateTime dateTime);

        void DeleteAll();
    }
}
