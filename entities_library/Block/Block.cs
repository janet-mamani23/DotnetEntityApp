public class Block
{
    public string BlockerUser {get; set;} //Usuario bloqueadior

    public string UserBlocked {get; set;} //Usuario bloqueado

    public required DateTime DateTimeStart {get; set;} //Fecha en que se blockea a un usuario.

    public required DateTime DateTimeEnd {get; set;} //Fecha en que se desblockea a un usuario.
}