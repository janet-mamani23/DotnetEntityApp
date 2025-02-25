using web_api.dto.common;

namespace web_api.dto.UserBanned;
public class UpdateBannedDTO : RequestDTO
{
    public required string NameUser {get; set;}
    public required string LastNameUser {get; set;}
 
}