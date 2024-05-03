using Task.DataAccess.Repositories.Interface;

namespace Task.DataAccess.Repositories.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IEmployeeRepository Employee { get; }
        public IUserRepository User { get; }
        public IDepartmentRepository Department { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Employee = new EmployeeRepository(_context);
            Department = new DepartmentRepository(_context);
            User = new UserRepository(_context);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
