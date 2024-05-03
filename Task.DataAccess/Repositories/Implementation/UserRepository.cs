using Task.DataAccess.Repositories.Interface;
using Task.Models;

namespace Task.DataAccess.Repositories.Implementation
{
    public class UserRepository(AppDbContext context) : Repository<User>(context), IUserRepository
    {
        public void Update(User obj)
        {
            context.Users.Update(obj);
        }
    }
}
