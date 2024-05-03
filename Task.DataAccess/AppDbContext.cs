using Microsoft.EntityFrameworkCore;
using Task.Models;

namespace Task.DataAccess
{
	public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Employee> Employees { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

		}
	}
}
