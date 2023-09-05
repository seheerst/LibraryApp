namespace LibraryApp.Models;

public interface IRentRepository : IRepository<Rent>
{
    void Edit(Rent rent);
    void Save();
}