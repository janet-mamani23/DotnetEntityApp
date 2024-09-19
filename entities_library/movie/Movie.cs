namespace entities_library.movie;
using entities_library.comment;
using entities_library.file_system;
using entities_library.Qualify;

public class Movie
{
    public long Id {get; set;}
    public required string Title {get; set;}
    public required string Description {get; set;}
    //Encapsulamiento: 
    public required Genero Genre {get; set;}
    //Genre es un atributo de Genero, osea Genero no es una propiedad de pelicula por que es un objeto. pelicula y genero son dos entities.
    public required FileEntity Image{get; set;}
    public required FileEntity Video{get; set;}
    public Qualify? Star {get; set;}
    public List<Comment> Comments { get; set; } = new List<Comment>();

}