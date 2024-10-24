using web_api.dto.common;
using web_api.dto.comment;
namespace web_api.dto.movie;
public class MoviesResponseDTO : ResponseDTO
{
    public long Id { get; set; } // Identificador de la película
    public string ?ImageUrl {get; set;}
}