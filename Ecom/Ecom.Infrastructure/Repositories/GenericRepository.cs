
using Ecom.Core.Interfaces;
using Ecom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ecom.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext _context;
    public GenericRepository(AppDbContext context)
    {
        _context = context?? throw new NullReferenceException();
    }


    //---------------------- Create ----------------------
    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }
 
    //---------------------- Read ----------------------
    public async Task<IReadOnlyList<T>> GetAllAsync()=>
        await _context.Set<T>().AsNoTracking().ToListAsync();

    public async Task<IReadOnlyList<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
    {
        var query = _context.Set<T>().AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.AsNoTracking().ToListAsync();

    }

    public async Task<T> GetByIdAsync(int id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        return entity;
    }
    public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _context.Set<T>();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        var entity = await query
            .FirstOrDefaultAsync(e =>
                EF.Property<int>(e, "Id") == id);

        return entity;
    }

    //---------------------- Update ----------------------
    public async Task UpdateAsync(T entity)
    {
        /*
        -> Entry(entity) returns the EntityEntry object for the entity you passed in.
        -> EntityState.Modified means that this entity is marked as Modified and needs to be updated in the database.
        -> EF Core does not execute the update immediately. Instead, it just tells the context:
            “When SaveChanges is called, generate an UPDATE statement for this entity.”
        */
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    //---------------------- Delete ----------------------
    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Set<T>().FindAsync(id) ??
            throw new KeyNotFoundException($"Entity with id {id} not found");


        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }
}
