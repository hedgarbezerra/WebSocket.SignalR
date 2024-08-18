using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;

namespace WebSocket.SignalR.Configuration.Middlewares.EntityFramework.Interceptors
{
    public class EfLoggingInterceptor : DbCommandInterceptor
    {
        public override DbCommand CommandInitialized(CommandEndEventData eventData, DbCommand result)
        {
            return base.CommandInitialized(eventData, result);
        }
        public override InterceptionResult<int> NonQueryExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<int> result)
        {
            Console.WriteLine($"Executing NonQuery: {command.CommandText}");
            return base.NonQueryExecuting(command, eventData, result);
        }
        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            Console.WriteLine($"Executing Reader: {command.CommandText}");
            return base.ReaderExecuting(command, eventData, result);
        }
    }
}
