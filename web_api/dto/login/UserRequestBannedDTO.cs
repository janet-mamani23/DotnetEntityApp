using entities_library.login;
using web_api.dto.common;

namespace web_api.dto.login;
public class UserRequestBannedDTO : ResponseDTO
{
    public required string NameUser {get; set;}
    public required User User {get;set;}
    public required string Reason {get;set;}
}