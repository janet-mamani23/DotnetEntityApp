using dao_library.Interfaces.comment;
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

    public async Task<Movie> Create(Movie movie)
    {
        if(context.Movies == null){
            throw new InvalidOperationException("Movies is null.");
        }
        await context.Movies.AddAsync(movie); // Agrega la película al contexto
        await context.SaveChangesAsync(); // Guarda los cambios en la base de datos

        return movie;
    }

    public async Task <bool> Delete (long id)
    {
        var movie = await context.Movies.FindAsync(id);
        if (movie == null) return false;

        context.Movies.Remove(movie);
        await context.SaveChangesAsync();
        return true; //Retorna true indicando éxito de la eliminacion.
    }

    public Task<Movie?> Get(string title, Genre genre)
    {
        throw new NotImplementedException();
    }

    public async Task<(IEnumerable<Movie>movies, int totalRecords)> GetAll(
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
        if (context.Movies == null) 
        {
            throw new InvalidOperationException("Movies set is not initialized.");
        }
        Movie? movie = await context.Movies
        .FirstOrDefaultAsync(m => m.Id == id);
        
        if (movie == null)
        {
            throw new KeyNotFoundException($"Movie with Id {id} not found");
        }
        
        return movie;
    }

    public async Task<Movie?> GetByTitle(string title)
    {
        return await context.Movies.FirstOrDefaultAsync(m => m.Title == title);
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