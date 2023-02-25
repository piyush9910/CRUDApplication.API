using CRUDApplication.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDApplication.API.Data
{
    public class CRUDApplicationDbContext : DbContext
    {
        public CRUDApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employee { get; set; }
    }
}
