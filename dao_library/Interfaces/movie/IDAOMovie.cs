using entities_library.movie;

namespace dao_library.Interfaces.movie;

public interface IDAOMovie
{
    Task<(IEnumerable<Movie>, int)> GetAll(
        string? query,
        int page,
        int pageSize
    ); //obtengo los movies
    Task<Movie> GetById(long id); //obtengo por id
    Task Save(Movie movie); //quiero guardar mi movie desde mi office
    Task Update(Movie movie); //quiero actualizar mi movie desde mi officce
    
    //NO PROGRAMAR
    Task Delete(Movie movie); //quiero eliminar mi movie desde mi office
    Task<(IEnumerable<Movie>,int)> GetTopRated(
        string? query,
        int page,
        int pageSize
    ); // obtengo las ej: 5 películas más calificadas
    Task<(IEnumerable<Movie>,int)> GetOscarWinners(
        string? query,
        int page,
        int pageSize
    ); //obtengo las mejores peliculas premiadas al oscar.
}  