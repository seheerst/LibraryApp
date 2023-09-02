using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using LibraryApp.Utility;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Models;

public class Repository<T>: IRepository<T> where T: class

{
    private readonly AppDbContext _context;
    internal DbSet<T> dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        this.dbSet = _context.Set<T>();
    }
   
    
    public IEnumerable<T> GetAll()
    {
        IQueryable<T> query = dbSet;
       return query.ToList();
    }

    public T Get(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = dbSet;
        query = query.Where(filter);
        return query.FirstOrDefault()!;
    }

    public void Add(T entity)
    {
        dbSet.Add(entity);
    }

    public void Delete(T entity)
    {
        dbSet.Remove(entity);
    }

    public void DeleteRange(IEnumerable<T> entities)
    {
        dbSet.RemoveRange(entities);
    }
}