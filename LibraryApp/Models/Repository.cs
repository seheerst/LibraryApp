using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using LibraryApp.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace LibraryApp.Models;

public class Repository<T>: IRepository<T> where T: class

{
    private readonly AppDbContext _context;
    internal DbSet<T> dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        this.dbSet = _context.Set<T>();
        _context.Books.Include(k => k.BookType).Include(k=> k.TypeId);
    }
   
    
    public IEnumerable<T> GetAll(string? includeProps = null)
    {
        IQueryable<T> query = dbSet;

        if (!string.IsNullOrEmpty(includeProps))
        {
            foreach (var inc in includeProps.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(inc);
            }
        }
        return query.ToList();
    }

    public T Get(Expression<Func<T, bool>> filter,string? includeProps = null)
    {
        IQueryable<T> query = dbSet;
        query = query.Where(filter);
        if (!string.IsNullOrEmpty(includeProps))
        {
            foreach (var inc in includeProps.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(inc);
            }
        }
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