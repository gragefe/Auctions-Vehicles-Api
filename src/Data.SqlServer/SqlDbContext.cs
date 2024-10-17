namespace Data.SqlServer;

using Data.SqlServer.Entities;
using Microsoft.EntityFrameworkCore;

public class SqlDbContext : DbContext
{
    public DbSet<Vehicle> Vehicles { get; set; }

    public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}