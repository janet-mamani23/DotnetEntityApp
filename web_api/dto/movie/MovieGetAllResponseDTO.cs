using web_api.dto.common;
namespace web_api.dto.movie;
public class MovieGetAllResponseDTO : ResponseDTO
{
    public required List<MoviesResponseDTO> Movies { get; set; }
    public int TotalRecords { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }

}