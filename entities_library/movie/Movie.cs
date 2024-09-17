namespace entities_library.movie;
using entities_library.comment;

public class Movie
{
    public long Id {get; set;}
    public string? title {get; set;}
    public string? description {get; set;}
    public string? gender {get; set;}
    public long star {get; set;}
    public List<Comment> Comments { get; set; } = new List<Comment>();

}