using entities_library.login;
using web_api.dto.common;

namespace web_api.dto.login;

public class LoginRequestDTO : RequestDTO
{
    public string EmailUser { get; set; } = "";

    public string PasswordUser { get; set; } = "";
}