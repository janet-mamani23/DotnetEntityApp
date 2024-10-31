namespace entities_library.movie;

using System;
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
    public virtual List<Comment> Comments { get; set; } = new List<Comment>();
    public virtual List<Qualify> Qualifies { get; set; } = new List<Qualify>();
    public bool HasOscar { get; set; } // Indica si la película ha ganado un Oscar

    public virtual double GetAverage()
    {
        if (Qualifies.Count == 0) return 0; // Evita la división por 0

        double suma = 0; // Inicializamos la suma
        foreach(Qualify star in Qualifies) // Iteramos sobre la lista de calificaciones
        {
           suma += star.Stars; // Sumamos las calificaciones de qualify
        }

        double average = suma / Qualifies.Count; // Calculamos el promedio
        return average; // Retornamos el promedio
    }

    public string GetImage()
    {
        if(this.Image != null)
        return this.Image.Path;
        return "";  //AGREGAR UNA URL GENERICO
    }

    public string GetVideo()
    {
        if(this.Video != null)
        return this.Video.Path;
        return ""; //AGREGAR UNA URL GENERICO
    }
}