using web_api.dto.common;
using entities_library.login;
using entities_library.movie;

namespace web_api.dto.qualify;

public class QualifyRequestDTO : RequestDTO
{
    public required int userId {get; set;}

    public required int star {get; set;}

    public required long movieId {get; set;}
}