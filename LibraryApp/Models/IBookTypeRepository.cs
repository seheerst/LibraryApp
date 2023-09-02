namespace LibraryApp.Models;

public interface IBookTypeRepository : IRepository<BookType>
{
    void Edit(BookType type);
    void Save();
}