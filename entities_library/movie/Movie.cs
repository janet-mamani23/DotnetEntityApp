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
    public virtual required Genre Genre {get; set;}
    public virtual required FileEntity Image{get; set;}
    public virtual required FileEntity Video{get; set;}
    public virtual List<Comment> Comments { get; set; } = new List<Comment>();
    public virtual List<Qualify> Qualifies { get; set; } = new List<Qualify>();
    public bool HasOscar { get; set; } 

    public virtual double GetAverage()
    {
        if (Qualifies.Count == 0) return 0;

        double suma = 0; 
        foreach(Qualify star in Qualifies) 
        {
           suma += star.Stars; 
        }
        double average = suma / Qualifies.Count; 
        return Math.Round(average,1); 
    }

    public string GetImage()
    {
        if(this.Image != null)
        return this.Image.Path;
        return "https://static.vecteezy.com/system/resources/previews/007/126/836/original/film-clapperboard-icon-vector.jpg"; 
    }

    public string GetVideo()
    {
        if(this.Video != null)
        return this.Video.Path;
        return "https://th.bing.com/th/id/OIP.wdY_mizIyp59YDFR48VaRAHaHE?rs=1&pid=ImgDetMain"; 
    }
}