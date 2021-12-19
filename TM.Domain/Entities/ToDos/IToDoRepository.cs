using System.Collections.Generic;
using System.Threading.Tasks;
using TM.Domain.Interfaces;

namespace TM.Domain.Entities.ToDos
{
    public interface IToDoRepository : IRepository<Todo>
    {
        public Task<IEnumerable<Todo>> GetTodos(int[] todoNos);

        public Task<IList<Todo>> GetTodos(int cardId);
    }
}
