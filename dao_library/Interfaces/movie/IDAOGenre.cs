using entities_library.movie;

namespace dao_library.Interfaces.movie;

public interface IDAOGenre
{
    Task<IEnumerable<Genre>> GetAll();
    Task<Genre> GetById(long id);
    Task Save(Genre genre);
    
    //NO PROGRAMAR
    Task Delete(Genre genre);
}