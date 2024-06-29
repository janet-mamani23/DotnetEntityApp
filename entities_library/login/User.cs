namespace entities_library.login;

public class User : Person
{
    public string Password { get; set;}

    public UserStatus UserStatus {get; set;} = UserStatus.Active;

    public entities_library.file_system.File? File {get; set;}

    public string Description {get; set;} = "";

     public List<ReportUser> ReportCounter {get; set;} = new List<ReportUser>(); //Va a llevar el conteo de reportes , si pasa los 3 reportes ese usuario sera banneado. Ademas de con la lista podremos acceder a detalles de cada reporte que se agrege a la lista.
}