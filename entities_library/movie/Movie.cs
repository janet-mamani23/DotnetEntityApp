namespace entities_library.movie;

public class Movie
{
    public long Id {get; set;}
    public string? title {get; set;}
    public string? description {get; set;}
    public string? Genero {get; set;}

    public long Star {get; set;}

    public List<Comment> Comments { get; set; } = new List<Comment>();

}