using web_api.dto.common;
using web_api.dto.comment;
namespace web_api.dto.movie;
public class MovieResponseDTO : ResponseDTO
{
    public long Id { get; set; } 
    public string ?Title { get; set; } 
    public string ?Genre { get; set; } 
    public string ?Description { get; set; } 
    public string ?ImageUrl {get; set;}
    public string ?VideoUrl { get; set; }
    public double? AverageQualify { get; set; } 
    public bool HasOscar{get;set;}
    public List<CommentResponseDTO> ?Comments { get; set; } = new List<CommentResponseDTO>(); 
}