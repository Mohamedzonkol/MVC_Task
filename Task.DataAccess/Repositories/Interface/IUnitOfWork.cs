namespace Task.DataAccess.Repositories.Interface
{
    public interface IUnitOfWork
    {
        IEmployeeRepository Employee { get; }
        IUserRepository User { get; }
        IDepartmentRepository Department { get; }
        void Save();
    }
}
