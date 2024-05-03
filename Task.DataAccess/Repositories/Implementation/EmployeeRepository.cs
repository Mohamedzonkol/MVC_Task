using Task.DataAccess.Repositories.Interface;
using Task.Models;

namespace Task.DataAccess.Repositories.Implementation
{
	public class EmployeeRepository(AppDbContext context) : Repository<Employee>(context), IEmployeeRepository
	{
		public void Update(Employee obj)
		{
			context.Employees.Update(obj);
		}
	}
}
