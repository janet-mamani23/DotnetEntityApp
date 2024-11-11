using web_api.dto.common;
namespace web_api.dto.movie;

public class GetAllMoviesRequestDTO: GetAllRequestDTO
{
    public string? Genre { get; set; }
    public bool HasOscar { get; set; } = true;
    public bool IsTopRated { get; set; } = true;
}