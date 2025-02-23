using web_api.dto.common;
namespace web_api.dto.UserBanned;
public class TotalResponseDTO : ResponseDTO
{
    public required List<GetAllResponseDTO> UsersBan { get; set; }

}