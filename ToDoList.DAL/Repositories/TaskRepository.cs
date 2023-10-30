using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using ToDoList.DAL.Interfaces;
using ToDoList.Domain.Entity;

namespace ToDoList.DAL.Repositories;

public class TaskRepository : IBaseRepository<TaskEntity>
{
    private readonly FirstAppDbContext firstAppDbContext;

    public TaskRepository(FirstAppDbContext firstAppDbContext)
    {
        this.firstAppDbContext = firstAppDbContext;
    }

    public async Task Create(TaskEntity entity)
    {
        await firstAppDbContext.Database.BeginTransactionAsync();
        await firstAppDbContext.Tasks.AddAsync(entity);
        await firstAppDbContext.SaveChangesAsync();
        await firstAppDbContext.Database.CommitTransactionAsync();
    }

    public IQueryable<TaskEntity> GetAll()
    {
        return firstAppDbContext.Tasks;
    }

    public async Task Delete(TaskEntity entity)
    {
        firstAppDbContext.Tasks.Remove(entity);
        await firstAppDbContext.SaveChangesAsync();
    }

    public async Task<TaskEntity> Update(TaskEntity entity)
    {
        firstAppDbContext.Tasks.Update(entity);
        await firstAppDbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<TaskEntity> GetByName(string name)
    {
        return await GetAll().FirstAsync(x => x.Name == name);
    }

    public async Task<TaskEntity> GetById(long id)
    {
        return await GetAll().FirstAsync(x => x.Id == id);
    }
}