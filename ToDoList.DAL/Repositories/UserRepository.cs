using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.DAL.Interfaces;
using ToDoList.Domain.Entity;

namespace ToDoList.DAL.Repositories;

public class UserRepository : IBaseRepository<UserEntity>
{
    private readonly FirstAppDbContext firstAppDbContext;

    public UserRepository(FirstAppDbContext firstAppDbContext)
    {
        this.firstAppDbContext = firstAppDbContext;
    }

    public async Task Create(UserEntity userEntity)
    {
        await firstAppDbContext.Users.AddAsync(userEntity);
        await firstAppDbContext.SaveChangesAsync();
    }

    public IQueryable<UserEntity> GetAll()
    {
        return firstAppDbContext.Users;
    }

    public async Task Delete(UserEntity userEntity)
    {
        firstAppDbContext.Users.Remove(userEntity);
        await firstAppDbContext.SaveChangesAsync();
    }

    public async Task<UserEntity> Update(UserEntity userEntity)
    {
        firstAppDbContext.Users.Update(userEntity);
        await firstAppDbContext.SaveChangesAsync();

        return userEntity;
    }

    public async Task<UserEntity> GetByName(string name)
    {
        return await GetAll().FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<UserEntity> GetById(long id)
    {
        return await GetAll().FirstOrDefaultAsync(x => x.Id == id);
    }
}