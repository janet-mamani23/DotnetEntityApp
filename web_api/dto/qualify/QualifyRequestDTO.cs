using web_api.dto.common;

namespace web_api.dto.qualify;

public class QualifyRequestDTO : RequestDTO
{
    public required long UserId {get; set;}

    public required int Star {get; set;}

    public required long MovieId {get; set;}
}