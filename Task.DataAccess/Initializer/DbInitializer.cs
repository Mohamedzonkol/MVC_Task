using Task.Models;

namespace Task.DataAccess.Initializer
{
    public class DbInitializer(AppDbContext context) : IDbInitializer
    {
        public void Initialize()
        {
            if (!context.Employees.Any())
            {
                var departments = new List<Department>()
            {
                new Department { Name = "IT", Description = "Information Technology" },
                new Department { Name = "HR", Description = "Human Resources" },
                new Department { Name = "Sales", Description = "Sales Department" }
            };
                context.Departments.AddRange(departments);
                context.SaveChanges();
                var users = new List<User>()
            {
                new User { Name = "John Doe", Email = "johndoe@company.com", Password = "password" },
                new User { Name = "Jane Smith", Email = "janesmith@company.com", Password = "password" }
            };
                context.Users.AddRange(users);
                context.SaveChanges();
                var employees = new List<Employee>()
            {
                new Employee
                {
                    Name = "Alice Johnson",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    PhoneNumber = "123-456-7890",
                    Email = "alice.johnson@company.com",
                    HireDate = DateTime.Now.AddMonths(-6),
                    Salary = 50000,
                    DepartmentID = departments[0].Id,
                    CreatedBy = users[0].Id,
                    ModifiedBy = users[0].Id
                },
                new Employee
                {
                    Name = "Bob Smith",
                    DateOfBirth = new DateTime(1985, 5, 15),
                    PhoneNumber = "987-654-3210",
                    Email = "bob.smith@company.com",
                    HireDate = DateTime.Now.AddMonths(-3),
                    Salary = 60000,
                    DepartmentID = departments[2].Id,
                    CreatedBy = users[1].Id,
                    ModifiedBy = users[1].Id
                }
            };
                context.Employees.AddRange(employees);

                context.SaveChanges();
            }
        }
    }
}

