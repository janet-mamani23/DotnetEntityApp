using web_api.dto.common;
namespace web_api.dto.UserBanned;

public class PutResponseDTO : ResponseDTO
{
    public long Id{get;set;}
    public required string NameUser {get; set;}
    public required string LastName {get;set;}
    public required string Avatar {get; set;}
}