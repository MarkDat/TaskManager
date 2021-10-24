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
    }
}
