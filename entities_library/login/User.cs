namespace entities_library.login;

public class User : Person
{
    public required string Password { get; set;}
    public file_system.File? Avatar {get; set;}
    public string? Description {get; set;}
    public void Encrypt(string password)
    {
        this.Password = this.encrypt(password);
    } 

    public bool IsPassword(string password)
    {
        return this.encrypt(password) == this.Password;
    }

    private string encrypt(string password)
    {
        //TODO - Fede: Averiguar como encriptar.
        return password;
    }
}