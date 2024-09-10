using web_api.dto.login;
using entities_library.comment;
using entities_library.login;
using entities_library.movie;

namespace web_api.mock;

public class CommentsMock
{
    private static CommentsMock? instance;

    private CommentsMock() { }

    public static CommentsMock Instance 
    {
        get
        {
            if(instance == null) instance = new CommentsMock();
            return instance;
        }
    }

    public List<Comment> _comments { get; set; } = new List<Comment>();

    public long CreateCommentMock
    ( string text, User user, Movie movie, DateTime createdAt)
    {
        Comment comment = new Comment
        {
            Text = text,
            User =  user,
            Movie = movie,
            CreatedAt = createdAt
        };

        comment.Id = this._comments.Count + 1;
        this._comments.Add(comment);

        return comment.Id;
    }

    public Comment? GetCommentById(long commentId)  //Busca un comentario por su id
    {
        return _comments.FirstOrDefault(m => m.Id == commentId);
    }

}