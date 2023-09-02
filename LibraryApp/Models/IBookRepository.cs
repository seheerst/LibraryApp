namespace LibraryApp.Models;

public interface IBookRepository : IRepository<Book>
{
    void Edit(Book book);
    void Save();
}