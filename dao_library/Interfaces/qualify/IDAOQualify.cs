using entities_library.login;
using entities_library.movie;
using entities_library.Qualify;


namespace dao_library.Interfaces.qualify;


public interface IDAOQualify
{  
    Task<Qualify?> GetById(long? id);

    Task Save(Qualify qualify);

   //NO PROGRAMAR
    public Task Delete(long id);

    Task<bool> Update(Qualify qualify);
    Task<IEnumerable<Qualify>> GetByMovieId(long movieId);

    Task<Qualify?> GetQualifyByUserAndMovie(long userId, long movieId);

    Task <bool> HasUserQualifiedMovie(User user, Movie movie); //comprueba si el usuario ya calificó la película
}
