using Microsoft.EntityFrameworkCore;

namespace EmployeeServices.Data
{
    public class EmployeeDBContext : DbContext
    {
        public EmployeeDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }


    }
}
