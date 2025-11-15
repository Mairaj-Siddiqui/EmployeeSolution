using Microsoft.EntityFrameworkCore;
using EmployeeApi.Models;
using System.Collections.Generic;

namespace EmployeeApi.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
    }

}
