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


    public async Task Delete(long id)
    {
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
/*
    public async Task<(IEnumerable<Qualify>,int)> GetAll(
        int qualifyId,
        int page,
        int pageSize
    )
    {
        IQueryable<Qualify>? qualifyQuery = context.Qualifies;       
        qualifyQuery = qualifyQuery.Where(p => p.Movie.Id == qualifyId)
        .Include(c => c.User)
        .Include(c => c.Movie);


        int totalQualifies = await qualifyQuery.CountAsync();


        var qualify = await qualifyQuery
        .Skip((page - 1)* pageSize)
        .Take(pageSize)
        .ToListAsync();


        return (qualify, totalQualifies);   
    }*/


    public Task<Qualify> GetById(long id)
    {
        Qualify? qualify = context.Qualifies?.Find(id);
        return Task.FromResult(qualify);
    }


    public async Task Save(Qualify qualify)
    {
        context.Qualifies?.Add(qualify);


        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Qualify>> GetUserQualificationsForMovie(long idUser, long idMovie)
    {
        return await context.Qualifies
            .Where(q => q.User.Id == idUser && q.Movie.Id == idMovie)
            .ToListAsync();
    }

    public async Task<bool> HasUserQualifiedMovie(string emailUser, long idMovie)
    {
        return await context.Qualifies
            .AnyAsync(q => q.User.Email == emailUser && q.Movie.Id == idMovie);
    }


    public async Task<IEnumerable<Qualify>> GetByMovieId(long movieId)
    {
        // Obtiene todas las calificaciones asociadas con una película
        var qualifies = await context.Qualifies
            .Where(q => q.Movie.Id == movieId)  // Filtra por ID de la película
            .Include(q => q.User)  // Incluye la información del usuario
            .ToListAsync();  // Convierte el resultado en una lista


        return qualifies;
    }

    public async Task<bool> Update(Qualify qualify)
    {
        var existingQualify = await context.Qualifies
            .FirstOrDefaultAsync(q => q.Id == qualify.Id);

        if (existingQualify != null)
        {
            existingQualify.Star = qualify.Star;
            context.Qualifies.Update(existingQualify);
            await context.SaveChangesAsync();
            return true;
        }
        return false;
    } 
}