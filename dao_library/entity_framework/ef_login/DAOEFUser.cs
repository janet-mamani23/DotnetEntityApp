using dao_library.Interfaces.login;
using entities_library.file_system;
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

    public async Task <bool> Delete(long id)
    {
         if (context.Users == null)
        {
            throw new InvalidOperationException("La colección de peliculas es nula.");
        }
        var user = await context.Users.FindAsync(id);
        if (user != null)
        {
            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return true;
        }
        else
        {
            return false;
        }
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


    public async Task<(IEnumerable<User> users, long totalRecords)> GetAll(string query, int page, int pageSize)
    {
        
        if (context.Users == null)
        {
           throw new InvalidOperationException("La colección de usuarios es nula.");
        }
        var lowerQuery = query.ToLower();

        IQueryable<User> listUsers = context.Users;
        if (query == "all")
        {
            long totalRecords = await listUsers.CountAsync(); 
            var users =  await listUsers
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
            
            return (users, totalRecords);
        }
        else
        {
            var filteredUsers = listUsers.Where(m => 
                m.Name.Contains(lowerQuery)
            );
            int totalRecords = await filteredUsers.CountAsync();
            if(totalRecords == 0)
            {   
                throw new InvalidOperationException("No hay usuarios con ese nombre.");
            }
            var users =  await filteredUsers
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (users, totalRecords);
        }  
    }

public async Task<(IEnumerable<User> users, long totalRecords)> GetAllAdmin(string query, int page, int pageSize)
    {
        
        if (context.Users == null)
        {
           throw new InvalidOperationException("La colección de usuarios es nula.");
        }
        var lowerQuery = query.ToLower();

        IQueryable<User> listUsers = context.Users;
        if (query == "all")
        {
            long totalRecords = await listUsers
                      .Where(user => user.IsAdmin)
                      .CountAsync();

            var admins = await listUsers
                      .Where(user => user.IsAdmin)
                      .Skip((page - 1) * pageSize)
                      .Take(pageSize)
                      .ToListAsync();
        
            return (admins, totalRecords);    
        }
        else
        {
            var filteredUsers = listUsers.Where(m => 
                m.Name.Contains(lowerQuery) && m.IsAdmin);

            int totalRecords = filteredUsers.Count();
            if(totalRecords == 0)
            {   
                throw new InvalidOperationException("No hay usuarios con ese nombre.");
            }
            var users =  await filteredUsers
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (users, totalRecords);
        }  
    }
    public async Task<User?> GetById(long id)
    {
        if (context.Users == null) return null;
        User? userId = await context.Users
            .Where(userId => userId.Id == id)
            .FirstOrDefaultAsync();

        return userId;
    }

    public async Task<User?> GetByUsername(string userName, string lastName)
    {
        if (userName == null) return null;
        if (context.Users == null) return null;

        User? user = await context.Users
                    .FirstOrDefaultAsync(user => user.Name == userName && user.LastName == lastName);
        return user;
    }

    public async Task<long> Save(User user)
    {
        context.Users?.Add(user);
        await context.SaveChangesAsync();
        return user.Id; 
    }
    public async Task Update(long userId, string? name, string? lastName, DateTime? birthdate, string? email, string? description)
    {
        if (context.Users != null)
        {   
            User? user = await context.Users
            .Where(user => user.Id == userId)
            .FirstOrDefaultAsync();
           
           if(user != null)
           {
                if (name != null)
                    user.Name = name;

                if (lastName != null)
                    user.LastName = lastName;

                if (birthdate != null)
                    user.Birthdate = (DateTime)birthdate;
                    
                if(email != null)
                    user.Email = email;

                if (description != null)
                    user.Description = description;
                    
                await context.SaveChangesAsync();
           } 
        }
        else
        {
            throw new InvalidOperationException("La coleccion de usuarios es nula.");
        }      
    }

    public async Task UpdateStatus(long userId,string cadena)
    {
        if (context.Users != null)
        {   
            User? user = await context.Users
            .Where(user => user.Id == userId)
            .FirstOrDefaultAsync();
           
           if(user != null && cadena == "activate")
           {
                user.UserStatus = UserStatus.Banned;
                await context.SaveChangesAsync();
           }  
           if(user != null && cadena == "deactivate")
           {
                user.UserStatus = UserStatus.Active;
                await context.SaveChangesAsync();
           }
        }
        else
        {
            throw new InvalidOperationException("La coleccion de usuarios es nula.");
        }      
    }
}