namespace Data.SqlServer;

using Data.SqlServer.Entities;
using Microsoft.EntityFrameworkCore;

public class SqlDbContext(DbContextOptions<SqlDbContext> options) : DbContext(options)
{
    public DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}