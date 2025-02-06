using entities_library.login;
using entities_library.file_system;

namespace dao_library.Interfaces.login;

public interface IDAOUser 
{
    Task<(IEnumerable<User>, int)> GetAll(
        string? query,
        int page,
        int pageSize
    );

    Task<User?> GetByUsername(string username);

    Task<User?> GetById(long? id);
    Task <long> Save(User user);
    
    //NO PROGRAMAR
    Task Delete(User user);
    Task<User?>Get(string emailUser, string password);
}