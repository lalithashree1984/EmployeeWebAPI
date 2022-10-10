using Microsoft.EntityFrameworkCore;

namespace EmployeeDetails.Data
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
