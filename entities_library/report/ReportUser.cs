using entities_library.login;


public class ReportUser
{
    public long Id {get; set;} //Identificacion del Reporte

    public User IdUser {get; set;} //Usuario que reporta.

    public User IdUsertoInspect {get; set;} //Usuario que será banneado.
    
}