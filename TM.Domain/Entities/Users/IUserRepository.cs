using System.Collections.Generic;
using System.Threading.Tasks;
using TM.Domain.Interfaces;

namespace TM.Domain.Entities.Users
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> NewUser(string userName
            , string password);
        Task<IEnumerable<User>> UsersInProjectAsync(int? projectId);
        User Login(string userName, string password);
    }
}
