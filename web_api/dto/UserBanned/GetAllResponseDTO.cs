using web_api.dto.common;
namespace web_api.dto.UserBanned;

public class GetAllResponseDTO : ResponseDTO
{
    public long Id{get;set;}
    public required string NameUser {get; set;}
    public required string LastName {get;set;}
    public required string Avatar {get; set;}
    public required DateTime? StartTime {get;set;}
    public required DateTime? EndTime {get;set;}
    public required string? Reason {get;set;}
}