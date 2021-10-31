using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TM.Domain.Interfaces;

namespace TM.API.Services
{
    public class BaseService
    {
        public BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected internal IUnitOfWork UnitOfWork { get; set; }
		protected async Task<T> ExecuteTransaction<T>(Func<Task<T>> action)
		{
			T result = default;

				try
				{
					await UnitOfWork.BeginTransaction();

					result = await action.Invoke();

					await UnitOfWork.CommitTransaction();
				}
				catch
				{
					await UnitOfWork.RollbackTransaction();
					throw;
				}

			return result;
		}
	}
}
