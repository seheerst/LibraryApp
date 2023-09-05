namespace LibraryApp.Models;

public interface IRentRepository : IRepository<Book>
{
    void Edit(Rent rent);
    void Save();
}