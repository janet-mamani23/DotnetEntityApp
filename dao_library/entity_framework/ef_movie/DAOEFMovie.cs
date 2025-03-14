using dao_library.Interfaces.comment;
using dao_library.Interfaces.movie;
using entities_library.comment;
using entities_library.file_system;
using entities_library.movie;
using entities_library.Qualify;
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
        // Paso 1: Aplicar los filtros usando el mismo método ApplyFilters
        var filteredMoviesQuery = ApplyFilters(query);

        // Paso 2: Obtener el total de registros
        int totalRecords = await GetTotalRecords(filteredMoviesQuery);

        // Paso 3: Aplicar la paginación
        var movies = await ApplyPagination(filteredMoviesQuery, page, pageSize).ToListAsync();

        // Retornar las películas y el total de registros
        return (movies, totalRecords);
    }
    
    public async Task<Movie?> GetById(long id)
    {
        if (context.Movies == null) 
        {
            throw new InvalidOperationException("Movies set is not initialized.");
        }
        Movie? movie = await context.Movies
        .FindAsync(id);
        
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
    public async Task UpdateQualify(long id, Qualify qualify)
    {
       if(context.Movies == null){
            throw new InvalidOperationException("La coleccion de Movies es nula.");
        }
        Movie? movie = await context.Movies
        .FirstOrDefaultAsync(m => m.Id == id);
        if(movie != null)
        {   
            movie.Qualifies.Add(qualify);
            context.Movies.Update(movie);
            await context.SaveChangesAsync(); 
        }
        else
        {
            throw new InvalidOperationException("Pelicula no encontrada.");
        }
    }

    public async Task Update(long movieId, string? title, string? description, Genre? genre, FileEntity? image, FileEntity? video, bool oscar)
    {
        if (context.Movies != null)
        {   
            Movie? movie = await context.Movies
            .Where(movie => movie.Id == movieId)
            .FirstOrDefaultAsync();
           
           if (movie != null)
            {
                if (title != null)
                    movie.Title = title;

                if (description != null)
                    movie.Description = description;

                if (genre != null)
                    movie.Genre = genre;

                if (image != null)
                    movie.Image = image;

                if (video != null)
                    movie.Video = video;

                movie.HasOscar = oscar; 

                await context.SaveChangesAsync();
            }
        }
        else
        {
            throw new InvalidOperationException("La coleccion de usuarios es nula.");
        }      
        
    }

    public async Task<(IEnumerable<Movie> movies, int totalRecords)> GetAllOscar(string query, int page, int pageSize)
    {
        // Paso 1: Aplicar los filtros
        var filteredMoviesQuery = ApplyFilters(query);

        // Paso 2: Obtener el total de registros
        int totalRecords = await GetTotalRecords(filteredMoviesQuery);

        // Paso 3: Aplicar la paginación
        var movies = await ApplyPagination(filteredMoviesQuery, page, pageSize).ToListAsync();

        // Retornar las películas y el total de registros
        return (movies, totalRecords);
    }
    public async Task<(IEnumerable<Comment> comments, int totalRecords)> GetCommentsForMovie(
    string movieTitle, 
    int page, 
    int pageSize)
    {
        if (context.Movies == null || context.Comments == null)
            {
                throw new InvalidOperationException("Las colecciones de películas o comentarios son nulas.");
            }
        // Buscar la película por título e incluir los comentarios
        var movie = await context.Movies
            .Include(m => m.Comments)
            .FirstOrDefaultAsync(m => m.Title.ToLower() == movieTitle.ToLower());
        if (movie == null)
            {
                throw new InvalidOperationException("No se encontró la película.");
            }
        // Paginación de los comentarios
        IQueryable<Comment> commentsQuery = context.Comments;
        var comments = await commentsQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        int totalRecords = await commentsQuery.CountAsync(); 
        return (comments, totalRecords);
    }

    public IQueryable<Movie> ApplyFilters(string query)
    {
        // Verificar que el contexto y las películas no sean nulos
        if (context?.Movies == null)
        {
            return Enumerable.Empty<Movie>().AsQueryable();
        }

        if (string.IsNullOrEmpty(query)  || query == "all")
        {
            return context.Movies;
        }

        IQueryable<Movie> moviesQuery = context.Movies;

        string lowerQuery = query.ToLower(); // Convertir una sola vez
        

        if (lowerQuery == "oscar")
        {
            moviesQuery = moviesQuery.Where(m => m.HasOscar); // Simplificar la condición booleana
        }
        else if (long.TryParse(query, out long genreId))
        {
            moviesQuery = moviesQuery.Where(m => m.Genre != null && m.Genre.Id == genreId && m.HasOscar); // Validar que Genre no sea nulo
        }
        else
        {
            moviesQuery = moviesQuery.Where(m => false); // Retornar vacío si no es un filtro válido
        }

        // Ejecutamos la consulta asincrónicamente
        return moviesQuery;
    }
    public async Task<int>GetTotalRecords(IQueryable<Movie> filteredMoviesQuery)
    {
        return await filteredMoviesQuery.CountAsync(); // Contar directamente en la base de datos
    }

    public IQueryable<Movie> ApplyPagination(IQueryable<Movie> filteredMoviesQuery, int page, int pageSize)
    {
        page = page < 1 ? 1 : page; // Asegurarse de que la página no sea menor que 1
        return filteredMoviesQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize);  // No llamar a ToList() aquí para mantenerlo como IQueryable
    }
}