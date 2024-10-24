using web_api.dto.common;
using entities_library.movie;

namespace web_api.dto.qualify;

public class QualifyResponseDTO : ResponsetDTO
{
    public long Id{get;set;}
    public required int Star {get; set;}
    public required int AverageStars {get; set;}
}