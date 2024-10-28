namespace entities_library.login;

public class UserBan{

    public long Id {get; set;}

    public DateTime? StartDateTime {get; set;}

    public DateTime? EndtDateTime {get; set;}

    public virtual required User User {get; set;}

    public string? Reason {get; set;} //Razon por la cual se banea.

}