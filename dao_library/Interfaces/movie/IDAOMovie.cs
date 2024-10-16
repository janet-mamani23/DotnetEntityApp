using entities_library.movie;

namespace dao_library.Interfaces.movie;

public interface IDAOMovie
{
    Task<IEnumerable<Movie>> GetAll(); //obtengo los movies
    Task<Movie> GetById(long id); //obtengo por id
    Task Save(Movie movie); //quiero guardar mi movie desde mi office
    Task Update(Movie movie); //quiero actualizar mi movie desde mi officce
    
    //NO PROGRAMAR
    Task Delete(Movie movie); //quiero eliminar mi movie desde mi office
    Task<IEnumerable<Movie>> GetTopRated(int count); // obtengo las ej: 5 películas más calificadas
    Task<IEnumerable<Movie>> GetOscarWinners(); //obtengo las mejores peliculas premiadas al oscar.
}  