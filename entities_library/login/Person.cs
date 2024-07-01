namespace entities_library.login;

public class Person
{   
    public UserStatus UserStatus {get; set;} = UserStatus.Active;
    public long Id {get; set;}

    public required string Name {get; set;}

    public required string LastName {get; set;}

    public required string Email {get; set;}

    public DateTime? Birthdate {get;set;}
}