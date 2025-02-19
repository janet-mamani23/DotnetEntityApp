using web_api.dto.common;
using web_api.dto.login;
namespace web_api.dto.user;
public class UserAllResponseDTO : ResponseDTO
{
    public required List<UserGetResponseDTO> Users { get; set; }
    public long TotalRecords { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }

}