using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Tests.Db;

public class FakeDbContext : DbContext
{
    public int SaveChanges()
    {
        return 0;
    }
    
    public DbSet<Log> Logs { get; set; }

}