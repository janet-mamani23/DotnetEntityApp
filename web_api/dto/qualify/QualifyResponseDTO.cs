using web_api.dto.common;
using entities_library.movie;

namespace web_api.dto.qualify;

public class QualifyResponseDTO : ResponseDTO
{
    public long id{get;set;}
    public required int star {get; set;}
    public required int averageStars {get; set;}
}