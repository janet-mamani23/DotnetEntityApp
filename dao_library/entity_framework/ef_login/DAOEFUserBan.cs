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

    public async Task Delete(long idUser)
    {
        if (context.UserBans == null)
        {
            throw new InvalidOperationException("La colección de Userbans es nula.");
        }
        var userBans = await context.UserBans
                                .Where(ub => ub.User.Id == idUser)
                                .ToListAsync();
        if (userBans.Any())
        {
            context.UserBans.RemoveRange(userBans);
            await context.SaveChangesAsync();
        }
        else
        {
            throw new InvalidOperationException("Usuario banneado no encontrado");
        }
    }

    public async Task<(IEnumerable<UserBan>usersBan, int totalBan)> GetAll()
    {
         try{
            if (context.UserBans == null)
            {
                throw new InvalidOperationException("La colección de Baneos es nula.");
            }
            IQueryable<UserBan> usersBanQuery= context.UserBans;
            var usersBan = await usersBanQuery.ToListAsync();
            int totalBan = await usersBanQuery.CountAsync();
            return (usersBan, totalBan);
            }
        catch (Exception)
            {
                int totalBan = 0;
                return (Enumerable.Empty<UserBan>(), totalBan);
            }
    }

    public async Task<UserBan?> GetByName(string name, string lastName)
    {
        if (name == null) return null;
        if (context.UserBans == null) return null;

        UserBan? userBan = await context.UserBans
                    .FirstOrDefaultAsync(userban => userban.User.Name == name && userban.User.LastName == lastName);

        return userBan;
    }

    public async Task Save(UserBan userBan)
    {
        context.UserBans?.Add(userBan);
        await context.SaveChangesAsync();
    }
}