using entities_library.login;
using web_api.dto.common;
namespace web_api.dto.login;
public class UserGetResponseDTO : ResponseDTO
{
    public long Id { get; set; }
    public required string Name { get; set; } 
    public required string LastName { get; set; } 
    public required string Avatar { get; set; }
    public required UserStatus UserStatus {get;set;}
}