using web_api.dto.common;
namespace web_api.dto.movie;
public class MoviesResponseDTO 
{
    public long Id { get; set; } // Identificador de la película
    public string ?ImageUrl {get; set;}
    public required string Name {get; set;}
    
}