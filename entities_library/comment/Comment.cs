using entities_library.login;

namespace entities_library.comment;

public class Comment
{
    public int Id { get; set; }
    public required string Text { get; set; }
    public int UserId { get; set; }
    public required User User { get; set; }
    public int MovieId { get; set; }
    public required Movie Movie { get; set; }
    public DateTime CreatedAt { get; set; }

}