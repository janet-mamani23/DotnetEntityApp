using web_api.dto.common;
namespace web_api.dto.movie;
public class MovieResponseDTO : ResponsetDTO
{
    public long Id { get; set; } // Identificador de la película
    public string ?Title { get; set; } // Título de la película
    public string ?Genre { get; set; } // Género de la película
    public string ?Description { get; set; } // Breve descripción de la película
}