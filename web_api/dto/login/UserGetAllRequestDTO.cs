using web_api.dto.common;

namespace web_api.dto.login;

public class UserGetAllRequestDTO : RequestDTO
{
    public required string Query { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}