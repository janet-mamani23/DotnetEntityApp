using entities_library.comment;
using entities_library.login;
using entities_library.movie;


namespace dao_library.Interfaces.comment;

public interface IDAOComment
{
    Task<(IEnumerable<Comment>,int)> GetAll(
        int movieId,
        int page,
        int pageSize
    );
    Task<Comment> GetById(long id);
    Task Save(Comment comment);
    
    //NO PROGRAMAR
    Task Delete(long id);

    Task Update(long id, string newText);
}