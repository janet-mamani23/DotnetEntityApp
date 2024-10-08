using Microsoft.EntityFrameworkCore;
using dao_library.Interfaces.comment;
using entities_library.comment;
using entities_library.login;
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
        var comment = await context.Comments.FindAsync(id);
        if (comment != null)
        {
            context.Comments.Remove(comment);
            await context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("Comentario no encontrado");
        }
    }

    public async Task<(IEnumerable<Comment>,int)> GetAll(
        int movieId,
        int page, 
        int pageSize
    )
    {
        IQueryable<Comment>? commentQuery = context.Comments;
        
        commentQuery = commentQuery.Where(p => p.Movie.Id == movieId)
        .Include(c => c.User)
        .Include(c => c.Movie);

        int totalRecords = await commentQuery.CountAsync();

        var comment = await commentQuery
        .Skip((page - 1)* pageSize)
        .Take(pageSize)
        .ToListAsync();

        return (comment, totalRecords);    
    }

    public Task<Comment> GetById(long id)
    {
        Comment? comment = context.Comments?.Find(id);
        return Task.FromResult(comment);
    }

    public async Task Save(Comment comment)
    {
        context.Comments?.Add(comment);

        await context.SaveChangesAsync();
    }

    public async Task Update(long id, string newText)
    {
        var comment = await context.Comments.FindAsync(id);
        if (comment != null)
        {
            comment.Text = newText;

            await context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("Comentario no encontrado");
        }
    }
}