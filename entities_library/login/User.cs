using System.Security.Cryptography;
using System.Text;
namespace entities_library.login;
using entities_library.file_system;

public class User : Person
{
    public required string Email { get; set; }
    public string Password { get; set;} = "";
    public virtual UserStatus userStatus { get; set; } = UserStatus.Active;
    public virtual FileEntity? Avatar {get; set;}
    public string Description {get; set;} = "";

    #region Methods
    public virtual void Encrypt(string password)
    {
        this.Password = this.encrypt(password);
    } 

    public virtual bool IsPassword(string password)
    {
        string passencrypt = this.encrypt(password);
        return passencrypt == this.Password;
    }

    private string encrypt(string password)
    {
        // Crea una instancia de SHA256 para aplicar el hash
        using (var sha256 = SHA256.Create())
        {
            // Convierte la contraseña en bytes y aplica el hash
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Convierte el hash a una cadena hexadecimal

            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
        //TODO - todos: Averiguar como encriptar.
        //return password.ToUpper();
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
            return "htttps://urlpaginacualquiera/userdefault.jpg";
            }
    }
#endregion
}