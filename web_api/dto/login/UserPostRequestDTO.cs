using web_api.dto.common;

namespace web_api.dto.login;

public class UserPostRequestDTO : RequestDTO
{
    public string Name { get; set; } = "";
    public string LastName { get; set; } = "";
    public DateTime? Birthdate { get; set; }
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
}