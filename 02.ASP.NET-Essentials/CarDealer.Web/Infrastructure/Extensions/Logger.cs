namespace CarDealer.Web.Infrastructure.Extensions
{
    using Services;
    using Services.Models.Enums;
    using System;
    using System.Security.Claims;

    public class Logger
    {
        private readonly ILogService logs;

        public Logger(ILogService logs)
        {
            this.logs = logs;
        }

        public void Create(ClaimsPrincipal user, string operation, string table)
        {
            var operationType = new OperationType();
            var modyfiedTable = new ModyfiedTable();

            switch (operation.ToLower())
            {
                case "create":
                    operationType = OperationType.Add;
                    break;
                case "edit":
                    operationType = OperationType.Edit;
                    break;
                case "delete":
                    operationType = OperationType.Delete;
                    break;
                default: throw new ArgumentException("Invalid Operation Type.");

            }

            switch (table.ToLower())
            {
                case "cars":
                    modyfiedTable = ModyfiedTable.Cars;
                    break;
                case "sales":
                    modyfiedTable = ModyfiedTable.Sales;
                    break;
                case "suppliers":
                    modyfiedTable = ModyfiedTable.Suppliers;
                    break;
                default: throw new ArgumentException("Invalid Operation Type.");
            }

            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            this.logs.Create(userId, operationType, modyfiedTable, DateTime.Now);
        }
    }
}
