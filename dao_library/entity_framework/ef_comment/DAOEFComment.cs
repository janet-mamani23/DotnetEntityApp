using Microsoft.EntityFrameworkCore;
using dao_library.Interfaces.comment;
using entities_library.comment;
using entities_library.movie;



namespace dao_library.entity_framework.ef_comment;

public class DAOEFComment: IDAOComment
{
    private readonly ApplicationDbContext context;

    public DAOEFComment(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task Delete(long id)
    {
        if (context.Comments != null)
        {
            var comment = await context.Comments.FindAsync(id);
            if (comment != null)
            {
                context.Comments.Remove(comment);
                await context.SaveChangesAsync();
            }
        }
        else
        {
            throw new Exception("Comentario no encontrado");
        }
    }

    public async Task<(IEnumerable<Comment>,int)> GetAll(
        Movie movie,
        int page, 
        int pageSize
    )
    {
        IQueryable<Comment>? commentQuery = context.Comments;
        if (commentQuery != null)
        {   commentQuery = commentQuery.Where(p => p.Movie == movie);
            
            int totalRecords = await commentQuery.CountAsync();

            var comment = await commentQuery
            .Skip((page - 1)* pageSize)
            .Take(pageSize)
            .ToListAsync();

            return (comment, totalRecords); 
        }  
        else 
        {
            throw new InvalidOperationException("La colección de comentarios es nula.");
        }
    }

    /*public async Task<List<Comment>> GetById(long id)
    {
        List<Comment?> comments = await context.Comments
            .Where(c => c.Movie == id)
            .ToListAsync();
        return comments;
    }*/

    public async Task Save(Comment comment)
    {
        context.Comments?.Add(comment);

        await context.SaveChangesAsync();
    }

    public async Task Update(long id, string newText)
    {
        if (context.Comments != null)
        {   var comment = await context.Comments.FindAsync(id);
            if (comment != null )
            {
                comment.Text = newText;
                await context.SaveChangesAsync();
            }
        }
        else
        {
            throw new Exception("Comentario no encontrado");
        }
    }
}