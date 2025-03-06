using BCrypt.Net;
namespace entities_library.login;
using entities_library.file_system;

public class User : Person
{
    public required string Email { get; set; }
    public string Password { get; set;} = "";
    public virtual UserStatus UserStatus { get; set; } = UserStatus.Active;
    public virtual FileEntity? Avatar {get; set;}
    public string Description {get; set;} = "";
    public bool IsAdmin { get; set; } = false;

    #region Methods
    public void SetPassword(string password)
    {
        this.Password = HashPassword(password);
    }

    public bool VerifyPassword(string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, this.Password);
    }

    private static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
    

    public string GetUserName()
    {
        if (this.Name != null)
            {
                return this.Name;
            }
        else 
            {
                return "No existe un nombre de usuario registrado";
            }
    }
    public string GetUrlAvatar()
    {   if (this.Avatar != null && this.Avatar.Path != null)
            {
            return this.Avatar.Path;
            }
        else 
            {
            return "https://img.freepik.com/vector-premium/icono-perfil-simple-color-blanco-icono_1076610-50204.jpg";
            }
    }
    public UserStatus GetUserStatus()
    {
        return UserStatus;
    }
#endregion
}