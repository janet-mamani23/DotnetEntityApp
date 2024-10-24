using web_api.dto.common;
using entities_library.login;
using entities_library.movie;

namespace web_api.dto.qualify;

public class QualifyRequestDTO : RequestDTO
{
    public required Person Name {get; set;}

    public required int Star {get; set;}    

    public required Movie Movie{get; set;}
}