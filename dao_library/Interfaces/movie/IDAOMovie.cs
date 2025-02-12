using entities_library.movie;

namespace dao_library.Interfaces.movie;

public interface IDAOMovie
{
    Task<(IEnumerable<Movie> movies, int totalRecords)> GetAll(
        string? query,
        int page,
        int pageSize
    ); //obtengo los movies
    Task<Movie> GetById(long id); //obtengo por id
    Task Save(Movie movie);
    Task Update(Movie movie); //quiero actualizar mi movie desde mi officce
    
    //NO PROGRAMAR
    Task<bool>Delete(long id); //quiero eliminar mi movie desde mi office
    Task<Movie?> GetByTitle(string title);
    Task<Movie> Create(Movie movie);  //quiero guardar mi movie desde mi office
}  