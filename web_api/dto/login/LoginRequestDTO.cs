using entities_library.login;
using web_api.dto.common;

namespace web_api.dto.login;

public class LoginRequestDTO : RequestDTO
{
    public string email { get; set; } = "";

    public string password { get; set; } = "";
}