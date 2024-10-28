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
    public virtual required Genre Genre {get; set;}
    //Genre es un atributo de Genero, osea Genero no es una propiedad de pelicula por que es un objeto. pelicula y genero son dos entities.
    public virtual required FileEntity Image{get; set;}
    public virtual required FileEntity Video{get; set;}
    public virtual User? User { get; set; }
    public virtual Qualify? Star {get; set;}
    public virtual List<Comment> Comments { get; set; } = new List<Comment>();
    public virtual List<Qualify> Stars { get; set; } = new List<Qualify>();
    public bool HasOscar { get; set; } // Indica si la película ha ganado un Oscar

    public virtual double GetAverage()
    {
        if (Stars.Count == 0) return 0; // Evita la división por 0


        double suma = 0; // Inicializamos la suma
        foreach(Qualify c in Stars) // Iteramos sobre la lista de calificaciones
        {
           suma += c.Star; // Sumamos las calificaciones
        }

        double average = suma / Stars.Count; // Calculamos el promedio
        return average; // Retornamos el promedio
    }

}