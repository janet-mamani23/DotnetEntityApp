using System.Security.Cryptography;
using System.Text;
namespace entities_library.login;

public class User : Person
{
    public required string Email { get; set; }
    public string Password { get; set;} = "";
    public UserStatus userStatus { get; set; } = UserStatus.Active;
    public file_system.FileEntity? Avatar {get; set;}
    public string Description {get; set;} = "";

    #region Methods
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
    #endregion
}