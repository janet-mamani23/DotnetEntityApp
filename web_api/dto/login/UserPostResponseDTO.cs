using web_api.dto.common;
namespace web_api.dto.login;
public class UserPostResponseDTO : ResponseDTO
{
    public long Id { get; set; }
    public string Name { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
}