using entities_library.comment;
using entities_library.login;
using entities_library.post;

public class ReportPost
{
    public long Id{get; set;}  
    public User IdUser{get; set;} //Usuario que realiza el reporte.
    public Post UserPost{get; set;} //Obtendremos todos los detalles del post reportado.
}