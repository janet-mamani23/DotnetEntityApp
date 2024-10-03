using dao_library.Interfaces.movie;
using entities_library.movie;

namespace dao_library.entity_framework.ef_movie;

public class DAOEFMovie: IDAOMovie
{
    private readonly ApplicationDbContext context;

    public DAOEFMovie(ApplicationDbContext context)
    {
        this.context = context;
    }

    public Task Delete(Movie movie)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Movie>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Movie> GetById(long id)
    {
        throw new NotImplementedException();
    }

    public Task Save(Movie movie)
    {
        throw new NotImplementedException();
    }
}