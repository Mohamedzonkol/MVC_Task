using Task.Models;

namespace Task.DataAccess.Repositories.Interface
{
	public interface IEmployeeRepository : IRepository<Employee>
	{
		void Update(Employee obj);
	}
}
