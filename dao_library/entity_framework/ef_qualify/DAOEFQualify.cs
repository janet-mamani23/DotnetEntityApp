using dao_library.Interfaces.qualify;
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

    public async Task<(IEnumerable<Qualify>,int)> GetAll(
        int movieId,
        int page, 
        int pageSize
    )
    {
        IQueryable<Qualify>? qualifyQuery = context.Qualifies;        
        qualifyQuery = qualifyQuery.Where(p => p.Movie.Id == movieId)
        .Include(c => c.User)
        .Include(c => c.Movie);

        int totalQualifies = await qualifyQuery.CountAsync();

        var qualify = await qualifyQuery
        .Skip((page - 1)* pageSize)
        .Take(pageSize)
        .ToListAsync();

        return (qualify, totalQualifies);    
    }

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

    Task IDAOQualify.Delete(Qualify qualify)
    {
        throw new NotImplementedException();
    }

    Task<IEnumerable<Qualify>> IDAOQualify.GetAll()
    {
        throw new NotImplementedException();
    }
}