using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Entities.ToDos;

namespace TM.Infrastructure.Data.Repositories
{
    public class TodoRepository : Repository<Todo>, IToDoRepository
    {
        public TodoRepository(TaskManagerContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Todo>> GetTodos(int[] todoNos)
        {
            return await Entities.Where(_ => todoNos.Contains(_.Id)).ToListAsync();
        }
    }
}
