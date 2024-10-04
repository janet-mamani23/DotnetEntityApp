using dao_library.Interfaces.login;
using entities_library.login;
using Microsoft.EntityFrameworkCore;

namespace dao_library.entity_framework.ef_login;

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
    public async Task<User?> Get(string userName, string password)
    {
        if(userName == null) return null;
        if(context.Users == null) return null;

        User? user = await context.Users
            .Where(user => user.Email.ToLower() == userName.ToLower())
            .FirstOrDefaultAsync();

        return user;
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