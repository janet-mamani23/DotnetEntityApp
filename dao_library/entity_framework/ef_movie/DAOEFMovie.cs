using dao_library.Interfaces.movie;
using entities_library.movie;
using Microsoft.EntityFrameworkCore;

namespace dao_library.entity_framework.ef_movie;

public class DAOEFMovie: IDAOMovie
{
    private readonly ApplicationDbContext context;

    public DAOEFMovie(ApplicationDbContext context)
    {
        this.context = context;
    }

    public Task Delete(Movie movie)
    {
        throw new NotImplementedException();
    }
    public async Task<(IEnumerable<Movie>,int)> GetAll(
        string? query, 
        int page, 
        int pageSize)
    {
        if (context.Movies == null)
        {
            return (Enumerable.Empty<Movie>(),0); // si mi context.movie es null devuelve una lista vacia.
        }

        IQueryable<Movie> moviesQuery = context.Movies;
        if (query != null)
        {
            moviesQuery = moviesQuery.Where(m => 
                m.Title.Contains(query) || m.Description.Contains(query)
            );
        }
        int totalRecords = await moviesQuery.CountAsync(); // Obtener el número total de películas antes de aplicar paginación

        var movies = await moviesQuery // Aplicar la paginación
            .Skip((page - 1) * pageSize) // Salta las películas de las páginas anteriores
            .Take(pageSize) // Tomar el número de películas especificadas por pageSize
            .ToListAsync(); // Ejecutar la consulta

        // Retornar las películas paginadas y el total de registros
        return (movies, totalRecords);
    }

    public async Task<Movie> GetById(long id)
    {
        if (context.Movies == null) //corrobora si los movies son null
        {
            throw new InvalidOperationException("Movies set is not initialized.");
        }
        var movie = await context.Movies
        .Include(m => m.Genre)       // Incluye el género
        .Include(m => m.Comments)    // Incluye los comentarios relacionados
        .Include(m => m.Image)       // Incluye la imagen
        .Include(m => m.Video)       // Incluye el video
        .Include(m => m.Star)        // Incluye las calificaciones (si es un objeto relacionado)
        .FirstOrDefaultAsync(m => m.Id == id); // Busca la película por su ID
        
        if (movie == null)
        {
            throw new KeyNotFoundException($"Movie with Id {id} not found");
        }
        return movie;
    }
    public async Task<(IEnumerable<Movie>,int)> GetOscarWinners(
        string? query, 
        int page, 
        int pageSize)
    {
        if (context.Movies == null)
        {
            return (Enumerable.Empty<Movie>(), 0); // Devuelve una lista vacía y un total de 0
        }
        IQueryable<Movie> oscarMoviesQuery = context.Movies
            .Where(m => m.HasOscar);

        if (query != null)
        {
            oscarMoviesQuery = oscarMoviesQuery.Where(m =>
                m.Title.Contains(query) || m.Description.Contains(query)
            );
        }

        int totalRecords = await oscarMoviesQuery.CountAsync();

        var oscarMovies = await oscarMoviesQuery
            .Skip((page - 1) * pageSize) // Saltar las películas de las páginas anteriores
            .Take(pageSize) // Tomar el número de películas especificadas por pageSize
            .ToListAsync();
            
        return (oscarMovies, totalRecords);
    }
    public async Task<(IEnumerable<Movie>,int totalRecords)> GetTopRated(
        string? query, 
        int page, 
        int pageSize
    )
    {
        if (context.Movies == null)
        {
        return (Enumerable.Empty<Movie>(),0); 
        }
        var totalRecords = await context.Movies.CountAsync();
        var movies = await context.Movies
            .OrderByDescending(m => m.Star) //Las películas se ordenan en orden descendente dependiendo Star
            .Skip((page - 1) * pageSize)
            .Take( pageSize) //cantidad de resultados a devolver
            .ToListAsync(); // Devuelve las count películas más calificadas
        
        return (movies, totalRecords);
    }

    public Task Save(Movie movie)
    {
        throw new NotImplementedException();
    }

    public Task Update(Movie movie)
    {
        throw new NotImplementedException();
    }

}