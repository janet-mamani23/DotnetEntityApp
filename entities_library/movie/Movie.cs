namespace entities_library.movie;
using entities_library.comment;
using entities_library.file_system;
using entities_library.login;
using entities_library.Qualify;

public class Movie
{
    public long Id {get; set;}
    public string Title {get; set;} = "";
    public  string Description {get; set;}  = "";
    //Encapsulamiento: 
    public required Genre Genre {get; set;}
    //Genre es un atributo de Genero, osea Genero no es una propiedad de pelicula por que es un objeto. pelicula y genero son dos entities.
    public required FileEntity Image{get; set;}
    public required FileEntity Video{get; set;}
    public required User User { get; set; }
    public Qualify? Star {get; set;}
    public List<Comment> Comments { get; set; } = new List<Comment>();
    public bool HasOscar { get; set; } // Indica si la película ha ganado un Oscar

}