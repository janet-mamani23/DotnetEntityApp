using entities_library.comment;

namespace entities_library.movie;
public class Movie
{
    public long Id {get; set;}

    public string Genre {get; set;}

    public string Name {get; set;}

    public string Actor {get; set;}

    public string Director {get; set;}
    public bool Oscar {get; set;}

    public long Star {get; set;}

    public List<Comment> Comments { get; set; } = new List<Comment>();

}