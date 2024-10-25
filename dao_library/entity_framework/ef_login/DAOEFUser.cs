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

    public async Task<User?> Get(string emailUser, string password)
    {
        if(emailUser == null) return null;
        if(context.Users == null) return null;

        User? user = await context.Users
            .Where(user => user.Email.ToLower() == emailUser.ToLower())
            .FirstOrDefaultAsync();

        return user;
    }


    public Task<(IEnumerable<User>, int)> GetAll(string? query, int page, int pageSize)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetById(long? id)
    {
        if (id == null) return null;
        if (context.Users == null) return null;
        User? userId = await context.Users
            .Where(userId => userId.Id == id)
            .FirstOrDefaultAsync();

        return userId;
    }

    public async Task<User?> GetByUsername(string username)
    {
        if (username == null) return null;
        if (context.Users == null) return null;

        User? user = await context.Users
            .Where(user => user.Email.ToLower() == username.ToLower())
            .FirstOrDefaultAsync();

        return user;
    }

    public Task Save(User user)
    {
        throw new NotImplementedException();
    }
}