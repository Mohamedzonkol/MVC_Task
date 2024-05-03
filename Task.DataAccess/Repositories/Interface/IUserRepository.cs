using Task.Models;

namespace Task.DataAccess.Repositories.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        void Update(User obj);
    }
}
