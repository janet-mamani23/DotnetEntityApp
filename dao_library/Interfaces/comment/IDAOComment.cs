using entities_library.comment;

namespace dao_library.Interfaces.comment;

public interface IDAOComment
{
    Task<IEnumerable<Comment>> GetAll();
    Task<Comment> GetById(long id);
    Task Save(Comment comment);
    
    //NO PROGRAMAR
    Task Delete(Comment comment);
}