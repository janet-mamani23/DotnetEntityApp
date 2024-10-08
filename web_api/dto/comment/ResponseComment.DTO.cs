namespace web_api.dto.comment;
using entities_library.login;
using entities_library.movie;

public class ResponseCommentDTO
{
    public string ?avatarUser {get; set;}  
    public required string idUser { get; set; }  
    public required string idMovie { get; set; }
    public required string text { get; set; }
}
