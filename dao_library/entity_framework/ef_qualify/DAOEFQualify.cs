using System.Reflection.Metadata.Ecma335;
using dao_library.Interfaces.qualify;
using entities_library.login;
using entities_library.movie;
using entities_library.Qualify;
using Microsoft.EntityFrameworkCore;

namespace dao_library.entity_framework.ef_qualify;

public class DAOEFQualify: IDAOQualify
{
    private readonly ApplicationDbContext context;

    public DAOEFQualify(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<Qualify?> GetById(long? id)
    {
        if (id == null) return null;
        if (context.Qualifies == null) return null;
        Qualify? qualifyId = await context.Qualifies
            .Where(qualifyId => qualifyId.Id == id)
            .FirstOrDefaultAsync();

        return qualifyId;
    }

    public async Task Delete(long id)
    {
        if (context.Qualifies == null)
        {
            throw new InvalidOperationException("La colección de Calificaciones es nula.");
        }
        var qualify = await context.Qualifies.FindAsync(id);
        if (qualify != null)
        {
            context.Qualifies.Remove(qualify);
            await context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("Calificación no encontrado");
        }
    }

    public async Task Save(Qualify qualify)
    {
        context.Qualifies?.Add(qualify);
        await context.SaveChangesAsync();
    }

    public async Task<bool> Update(Qualify qualify)
    {
        if (context.Qualifies != null)
        {   
           var existingQualify = await context.Qualifies
            .FirstOrDefaultAsync(q => q.Id == qualify.Id);

            if (existingQualify != null)
            {
                existingQualify.Stars = qualify.Stars;
                context.Qualifies.Update(existingQualify);
                await context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            throw new InvalidOperationException("La coleccion de Calificaiones es nula.");
        }      
    }

    public async Task<IEnumerable<Qualify>> GetByMovieId(long movieId)
    {
        // Obtiene todas las calificaciones asociadas con una película
        if (context.Qualifies != null)
        {   
           var qualifies = await context.Qualifies
            .Where(q => q.Movie.Id == movieId)  // Filtra por ID de la película
            .Include(q => q.User)  // Incluye la información del usuario
            .ToListAsync();  // Convierte el resultado en una lista
            return qualifies;
        }
        else
        {
            throw new InvalidOperationException("La coleccion de Calificaiones es nula.");
        }
        
    }

    public async Task<Qualify?> GetQualifyByUserAndMovie(long userId, long movieId)
    {
        if (context.Qualifies != null)
        {   
            return await context.Qualifies
            .FirstOrDefaultAsync(q => q.UserId == userId && q.MovieId == movieId);
        }
        else
        {
            throw new InvalidOperationException("La coleccion de Calificaiones es nula.");
        }
        
    }

    public async Task<bool> HasUserQualifiedMovie(User user, Movie movie)
    {
        if (context.Qualifies != null)
        {   
            return await context.Qualifies
            .AnyAsync(q => q.User.Name == user.Name && q.Movie.Id == movie.Id);
        }
        else
        {
            throw new InvalidOperationException("La coleccion de Calificaiones es nula.");
        }

    }
}