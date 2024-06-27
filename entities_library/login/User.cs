using System.ComponentModel;

namespace entities_library.login;

public class User : Persona
{
    public string Password { get; set; }

    public UserStatus UserStatus {get; set;} = UserStatus.Active;

    public entities_library.file_system.File? File {get; set;}

    public string Description {get; set;} = "";
}