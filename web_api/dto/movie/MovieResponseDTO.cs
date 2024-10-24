using web_api.dto.common;
using web_api.dto.comment;
namespace web_api.dto.movie;
public class MovieResponseDTO : ResponseDTO
{
    public long Id { get; set; } // Identificador de la película
    public string ?Title { get; set; } // Título de la película
    public string ?Genre { get; set; } // Género de la película
    public string ?Description { get; set; } // Breve descripción de la película
    public string ?ImageUrl {get; set;}
    public string ?VideoUrl { get; set; }
    public int ? Star {get; set;}
    public List<CommentResponseDTO> ?Comments { get; set; } = new List<CommentResponseDTO>(); // Lista de comentarios
}