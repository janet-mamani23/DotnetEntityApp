using entities_library.login;
using entities_library.movie;

namespace entities_library.comment;

public class Comment
{
    public long Id { get; set; }
    public string Text { get; set; } = "";
    public User User { get; set; }
    public Movie Movie { get; set; }
    public DateTime CreatedAt { get; set; }

    public Comment(string text, User user, Movie movie, DateTime createdAt)
    {
        Text = text;
        User = user;
        Movie = movie;
        CreatedAt = createdAt;
    }

}