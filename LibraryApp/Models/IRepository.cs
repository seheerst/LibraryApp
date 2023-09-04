using System.Linq.Expressions;

namespace LibraryApp.Models;

public interface IRepository<T> where T : class
{
    // T sınıf yerine geçer

    IEnumerable<T> GetAll(string? includeProps = null);

    T Get(Expression<Func<T, bool>> filter,string? includeProps = null);

    void Add(T entity);
    void Delete(T entity);
    void DeleteRange(IEnumerable<T> entities);

}