using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Entity;

namespace ToDoList.DAL;

public class FirstAppDbContext : DbContext
{
    public FirstAppDbContext(DbContextOptions<FirstAppDbContext> options) : base(options)
    {
        
        Database.EnsureCreated();
    }
    
    public DbSet<TaskEntity> Tasks { get; set; }

    public DbSet<ProfileEntity> Profiles { get; set; }

    public DbSet<UserEntity> Users { get; set; }
}