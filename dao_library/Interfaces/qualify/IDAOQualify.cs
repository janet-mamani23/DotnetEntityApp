using entities_library.Qualify;

namespace dao_library.Interfaces.qualify;

public interface IDAOQualify
{   
    Task<Qualify> GetById(long id);
    Task Save(Qualify qualify);
    
    //NO PROGRAMAR
    public Task Delete(long id);

    Task<bool> Update(Qualify qualify);
    Task<IEnumerable<Qualify>> GetByMovieId(long movieId);

    Task <bool> HasUserQualifiedMovie(long userId, long movieId);
}