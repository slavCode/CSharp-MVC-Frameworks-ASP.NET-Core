namespace CarDealer.Services.Models.Logs
{
    using Enums;
    using System;

    public class LogModel
    {
        public string User { get; set; }

        public OperationType Operation { get; set; }

        public ModyfiedTable Table { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
