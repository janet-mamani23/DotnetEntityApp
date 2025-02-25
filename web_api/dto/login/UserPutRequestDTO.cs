using web_api.dto.common;

namespace web_api.dto.login;

public class UserPutRequestDTO : RequestDTO
{
    public required long IdUser {get;set;}
    public string? Name { get; set; } = "";
    public string? LastName { get; set; } = "";
    public DateTime? Birthdate { get; set; }
    public string? Email { get; set; } = "";
    public string? Description {get;set;} = "";
}