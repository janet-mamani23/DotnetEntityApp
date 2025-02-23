using web_api.dto.common;
namespace web_api.dto.qualify;

public class QualifyResponseDTO : ResponseDTO
{
    public long Id{get;set;}
    public required int Star {get; set;}
    public required double AverageStars {get; set;}
}