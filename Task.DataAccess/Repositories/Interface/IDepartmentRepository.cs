using Task.Models;

namespace Task.DataAccess.Repositories.Interface
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        void Update(Department obj);
    }
}
