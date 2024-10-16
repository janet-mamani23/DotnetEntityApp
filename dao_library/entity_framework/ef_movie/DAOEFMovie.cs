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

    public async Task<IEnumerable<Movie>> GetAll() // Llama al GetAll en el DAO
    {
        if (context.Movies == null)
    {
        return Enumerable.Empty<Movie>(); // si mi context.movie es null devuelve una lista vacia.
    }

    return await context.Movies.ToListAsync(); //caso contrario devuelve una lista de peliculas.
        
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
    
    public async Task<IEnumerable<Movie>> GetOscarWinners()
    {
        if (context.Movies == null)
        {
            return Enumerable.Empty<Movie>();
        }
        return await context.Movies
            .Where(m => m.HasOscar) 
            .ToListAsync();
    }

    public async Task<IEnumerable<Movie>> GetTopRated(int count)
    {
        if (context.Movies == null)
    {
        return Enumerable.Empty<Movie>(); 
    }
        return await context.Movies
            .OrderByDescending(m => m.Star) //Las películas se ordenan en orden descendente dependiendo Star
            .Take( count) //luego se define la cantidad de resultados a devolver
            .ToListAsync(); // Devuelve las count películas más calificadas
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