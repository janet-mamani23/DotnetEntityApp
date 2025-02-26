using web_api.dto.common;
namespace web_api.dto.movie;
public class MoviesResponseDTO : ResponseDTO
{
    public long Id { get; set; } 
    public required string ImageUrl {get; set;}
    public required string Name {get; set;}
    
}