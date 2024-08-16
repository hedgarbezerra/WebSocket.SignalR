using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;

namespace WebSocket.SignalR.Configuration.Middlewares.EntityFramework.ExecutionStrategies
{
    public class CustomRetryExecutionStrategy : ExecutionStrategy
    {
        private readonly List<int> _retriableErrors;

        public CustomRetryExecutionStrategy(ExecutionStrategyDependencies dependencies, int maxRetryCount, TimeSpan maxRetryDelay, List<int> retriableErrors) 
            : base(dependencies, maxRetryCount, maxRetryDelay)
        {
            _retriableErrors = retriableErrors;
        }

        protected override bool ShouldRetryOn(Exception exception)
        {
            var sqlException = exception as SqlException;
            if(sqlException is null)
                return false;

            return _retriableErrors.Exists(e => sqlException.ErrorCode == e);
        }
    }
}
