using entities_library.login;
using entities_library.movie;

namespace entities_library.comment;

public class Comment
{
    public long Id { get; set; }
    public required string Text { get; set; }
    public  required User User { get; set; }
    public required Movie Movie { get; set; }
    public DateTime CreatedAt { get; set; }

}