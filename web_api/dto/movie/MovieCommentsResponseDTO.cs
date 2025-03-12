namespace web_api.dto.comment;
using entities_library.login;
using entities_library.movie;
using web_api.dto.common;

public class MovieCommentsResponseDTO : ResponseDTO
{
    public required List<CommentResponseDTO> Comments { get; set; }
    public long TotalRecord {get;set;}
}