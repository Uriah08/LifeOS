using LifeOS.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace LifeOS.Infrastructure.Data;

public class LifeOSDbContext : DbContext
{
    public LifeOSDbContext(DbContextOptions<LifeOSDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<TaskItem> Tasks { get; set; }
}