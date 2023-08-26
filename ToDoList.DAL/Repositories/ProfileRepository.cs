using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.DAL.Interfaces;
using ToDoList.Domain.Entity;

namespace ToDoList.DAL.Repositories;

public class ProfileRepository : IBaseRepository<ProfileEntity>
{
    private readonly FirstAppDbContext firstAppDbContext;

    public ProfileRepository(FirstAppDbContext firstAppDbContext)
    {
        this.firstAppDbContext = firstAppDbContext;
    }

    public async Task Create(ProfileEntity profileEntity)
    {
        await firstAppDbContext.Profiles.AddAsync(profileEntity);
        await firstAppDbContext.SaveChangesAsync();
    }

    public IQueryable<ProfileEntity> GetAll()
    {
        return firstAppDbContext.Profiles;
    }

    public async Task Delete(ProfileEntity profileEntity)
    {
        firstAppDbContext.Profiles.Remove(profileEntity);
        await firstAppDbContext.SaveChangesAsync();
    }

    public async Task<ProfileEntity> Update(ProfileEntity profileEntity)
    {
        firstAppDbContext.Profiles.Update(profileEntity);
        await firstAppDbContext.SaveChangesAsync();

        return profileEntity;
    }

    public async Task<ProfileEntity> GetByName(string name)
    {
        return await GetAll().FirstAsync(x => x.Name == name);
    }

    public async Task<ProfileEntity> GetById(long id)
    {
        return await GetAll().FirstAsync(x => x.Id == id);
    }
}
