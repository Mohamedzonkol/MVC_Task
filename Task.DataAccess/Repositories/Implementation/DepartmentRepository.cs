using Task.DataAccess.Repositories.Interface;
using Task.Models;

namespace Task.DataAccess.Repositories.Implementation
{
    public class DepartmentRepository(AppDbContext context) : Repository<Department>(context), IDepartmentRepository
    {
        public void Update(Department obj)
        {
            context.Departments.Update(obj);
        }
    }
}
