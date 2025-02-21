using dao_library.Interfaces.login;
using entities_library.login;
using Microsoft.EntityFrameworkCore;

namespace dao_library.entity_framework.ef_login;

public class DAOEFUserBan : IDAOUserBan
{
    private readonly ApplicationDbContext context;

    public DAOEFUserBan(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task Delete(long id)
    {
        if (context.UserBans == null)
        {
            throw new InvalidOperationException("La colección de Userbans es nula.");
        }
        var userBan = await context.UserBans.FindAsync(id);
        if (userBan != null)
        {
            context.UserBans.Remove(userBan);
            await context.SaveChangesAsync();
        }
        else
        {
            throw new InvalidOperationException("Usuario banneado no encontrado");
        }
    }

    public async Task<IEnumerable<UserBan>> GetAll()
    {
         try{
            if (context.UserBans == null)
            {
                throw new InvalidOperationException("La colección de Baneos es nula.");
            }
            var userBan = await context.UserBans.ToListAsync();
            return userBan;
            }
        catch (Exception ex)
            {
                Console.WriteLine($"Error getAll genre: {ex.Message}");
                return Enumerable.Empty<UserBan>();
            }
    }

    public Task<UserBan> GetById(long id)
    {
        throw new NotImplementedException();
    }

    public Task Save(UserBan userBan)
    {
        throw new NotImplementedException();
    }
}