using entities_library.comment;
using entities_library.login;
using entities_library.reaction;

namespace entities_library.post;

public class Post
{
    public long Id {get; set;}

    public required string Text {get; set;}


    public List<Comment> Comments {get; set;} = new List<Comment>();

    public List<Reaction> Reactions {get; set;} = new List<Reaction>();

    public DateTime DateTime {get; set;}

    public required User User {get; set;} //Almcenará un objeto de tipo User.

    public PostStatus PostStatus {get; set;} // El usuario esta activo o no.

    public ReportStatus ReportStatus {get; set;} // Obtendremos el valor del enum "ReportStatus". Para condicionar si pasa tal cantidad , modificamos en PostStatus en activo.
}