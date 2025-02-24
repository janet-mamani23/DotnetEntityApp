using dao_library.Interfaces.movie;
using entities_library.movie;
using Microsoft.EntityFrameworkCore;

namespace dao_library.entity_framework.ef_movie;

public class DAOEFGenre: IDAOGenre
{
    private readonly ApplicationDbContext context;

    public DAOEFGenre(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<bool> Delete(long id)
    {
        if (context.Genres == null)
        {
            throw new InvalidOperationException("La colección de géneros es nula.");
        }
        var genre = await context.Genres.FindAsync(id);
        if (genre != null)
        {
            context.Genres.Remove(genre);
            await context.SaveChangesAsync();
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<IEnumerable<Genre?>> GetAll()
    {
        try{
            if (context.Genres == null)
            {
                throw new InvalidOperationException("La colección de géneros es nula.");
            }
            var genres = await context.Genres.ToListAsync();
            return genres;
            }
        catch (Exception ex)
            {
                Console.WriteLine($"Error getAll genre: {ex.Message}");
                return Enumerable.Empty<Genre?>();
            }
    }

    public async Task<Genre?> GetById(long? id)
    {
        if (id == null) return null;
        if (context.Genres == null) return null;
        Genre? genreId = await context.Genres
            .Where(genreId => genreId.Id == id)
            .FirstOrDefaultAsync();

        return genreId;

    }

    public async Task<long?>Save(Genre genre)
    {   
        try{
            context.Genres?.Add(genre);
            await context.SaveChangesAsync(); 
            return genre.Id; 
            }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving genre: {ex.Message}");
            return null;
        }
    }
}