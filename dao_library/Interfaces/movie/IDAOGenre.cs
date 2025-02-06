using entities_library.movie;

namespace dao_library.Interfaces.movie;

public interface IDAOGenre
{
    Task<IEnumerable<Genre?>> GetAll();
    Task<Genre?> GetById(long? id);
    Task<long?>Save(Genre genre);
    
    //NO PROGRAMAR
    Task<bool> Delete(long id);
}