using web_api.dto.common;
namespace web_api.dto.movie;

public class MovieDeleteResponseDTO : MoviesResponseDTO
{
     public bool Success { get; set; } = false;

    public string Message { get; set; } = "";
}