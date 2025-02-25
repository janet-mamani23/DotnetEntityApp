using web_api.dto.common;

namespace web_api.dto.login;

public class UserGetRequestDTO : RequestDTO
{
   public required long UserId {get;set;}
}