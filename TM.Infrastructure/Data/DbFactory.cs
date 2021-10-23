using System;
using Microsoft.EntityFrameworkCore;

namespace TM.Infrastructure.Data
{
    public class DbFactory : IDisposable
    {
        private bool _disposed;
        private Func<TaskManagerContext> _instanceFunc;
        private DbContext _dbContext;
        public DbContext DbContext => _dbContext ?? (_dbContext = _instanceFunc.Invoke());

        public DbFactory(Func<TaskManagerContext> dbContextFactory)
        {
            _instanceFunc = dbContextFactory;
        }

        public void Dispose()
        {
            if (!_disposed && _dbContext != null)
            {
                _disposed = true;
                _dbContext.Dispose();
            }
        }
    }
}
