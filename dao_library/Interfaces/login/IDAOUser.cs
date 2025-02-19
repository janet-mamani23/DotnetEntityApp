using entities_library.login;
using entities_library.file_system;
using entities_library.movie;

namespace dao_library.Interfaces.login;

public interface IDAOUser 
{
    Task<(IEnumerable<User> users, long totalRecords)> GetAll(
        string query,
        int page,
        int pageSize
    );

    Task<User?> GetByUsername(string username);

    Task<User?> GetById(long id);
    Task <long> Save(User user);
    
    //NO PROGRAMAR
    Task <bool> Delete(long id);
    Task<User?>Get(string emailUser, string password);
    Task Update(long userId, string name, string lastName, DateTime birthdate, string email, string description);
}