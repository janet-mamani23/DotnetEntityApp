using entities_library.movie;

namespace dao_library.Interfaces.movie;

public interface IDAOMovie
{
    Task<IEnumerable<Movie>> GetAll();
    Task<Movie> GetById(long id);
    Task Save(Movie movie);
    
    //NO PROGRAMAR
    Task Delete(Movie movie);
}