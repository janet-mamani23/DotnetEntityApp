using entities_library.login;
using web_api.dto.common;

namespace web_api.dto.UserBanned;
public class UserRequestBannedDTO : RequestDTO
{
    public required string NameUser {get; set;}
    public required string LastNameUser {get; set;}
    public required string Reason {get;set;}
}