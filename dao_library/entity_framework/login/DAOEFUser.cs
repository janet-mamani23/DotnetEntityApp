using dao_library.Interfaces.login;
using entities_library.login;

namespace dao_library.entity_framework.login;

public class DAOEFUser : IDAOUser
{
    private readonly ApplicationDbContext context;

    public DAOEFUser(ApplicationDbContext context)
    {
        this.context = context;
    }

    public Task Delete(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User> Get(string userName, string password)
    {
        throw new NotImplementedException();
    }

    public Task<(IEnumerable<User>, int)> GetAll(string? query, int page, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetById(long id)
    {
        throw new NotImplementedException();
    }

    public Task Save(User user)
    {
        throw new NotImplementedException();
    }
}