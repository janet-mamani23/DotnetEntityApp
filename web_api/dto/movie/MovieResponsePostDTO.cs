using web_api.dto.common;
using web_api.dto.comment;
namespace web_api.dto.movie;
public class MovieResponsePostDTO : ResponseDTO
{
    public long Id { get; set; } 
    public required string Title { get; set; } 
    public required string Genre { get; set; } 
    public required string Description { get; set; } 
    public required string ImageUrl {get; set;}
    public required string VideoUrl { get; set; }
    public bool HasOscar{get;set;}

}