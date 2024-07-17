using entities_library.login;

public class Follow
{
     public long Id { get; set; }
    public User UserFrom{get; set;} //Usuario que realiza la accion.

    public User DestinationUser{get; set;} // Usuario a quien sigue.

    public required DateTime DateTime {get; set;}

}