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

    public async Task<Movie?> Save(Movie movie)
    {
        if(context.Movies == null){
            throw new InvalidOperationException("Movies is null.");
        }
        await context.Movies.AddAsync(movie); // Agrega la película al contexto
        await context.SaveChangesAsync(); // Guarda los cambios en la base de datos
        return movie;
    }

    public async Task<bool> Delete(long id)
    {
        if (context.Movies == null)
        {
            throw new InvalidOperationException("La colección de peliculas es nula.");
        }
        var movie = await context.Movies.FindAsync(id);
        if (movie != null)
        {
            context.Movies.Remove(movie);
            await context.SaveChangesAsync();
            return true;
        }
        else
        {
            return false;
        }
    }


    public async Task<(IEnumerable<Movie>movies, int totalRecords)> GetAll(
        string query, 
        int page, 
        int pageSize)
    {
        if (context.Movies == null)
        {
           throw new InvalidOperationException("La colección de peliculas es nula.");
        }
        var lowerQuery = query.ToLower();

        IQueryable<Movie> moviesQuery = context.Movies;
        if (query == "all")
        {
            var movies =  await moviesQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
            int totalRecords = await moviesQuery.CountAsync(); 
            return (movies, totalRecords);
        }
        else
        {
            long result = long.Parse(query);
            moviesQuery = moviesQuery.Where(m => 
                m.Genre.Id == result
            );
            var movies =  await moviesQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
            int totalRecords = await moviesQuery.CountAsync(); 
            return (movies, totalRecords);
        }  
    }

    public async Task<Movie?> GetById(long id)
    {
        if (context.Movies == null) 
        {
            throw new InvalidOperationException("Movies set is not initialized.");
        }
        Movie? movie = await context.Movies
        .FirstOrDefaultAsync(m => m.Id == id);
        
        if (movie == null)
        {
            return null;
        }
        return movie;
    }

    public Task<Movie?> GetByTitle(string title)
    {
         throw new NotImplementedException();
    }

    public async Task<Movie?> ExistMovie(string title)
    {
        if (context.Movies == null) 
        {
            throw new InvalidOperationException("Movies set is not initialized.");
        }
        
        Movie? movie = await context.Movies
        .FirstOrDefaultAsync(m => m.Title == title);
        if(movie != null)
            {
                return movie;
            }
        else
            {
                return null;
            }
    }
    //TODO completar
    public Task Update(Movie movie)
    {
        throw new NotImplementedException();
    }

}