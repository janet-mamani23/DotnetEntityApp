using entities_library.movie;
using entities_library.Qualify;
using entities_library.file_system;
using entities_library.comment;
namespace dao_library.Interfaces.movie;

public interface IDAOMovie
{
    Task<(IEnumerable<Movie> movies, int totalRecords)> GetAll(
        string query,
        int page,
        int pageSize
    ); 

    Task<Movie?> GetById(long id); //obtengo por id
    Task <Movie?> Save(Movie movie); //quiero guardar mi movie desde mi office
    Task UpdateQualify (long id, Qualify qualify); //quiero actualizar mi movie desde mi officce
    
    Task Update (long movieId, string? title, string? description, Genre? genre, FileEntity? image, FileEntity? video, bool oscar);
    Task<bool> Delete(long id); //quiero eliminar mi movie desde mi office
    Task<Movie?> GetByTitle(string title);
    Task<Movie?> ExistMovie (string title);
    Task<(IEnumerable<Comment> comments, int totalRecords)> GetCommentsForMovie(
    string movieTitle, 
    int page, 
    int pageSize);
}  