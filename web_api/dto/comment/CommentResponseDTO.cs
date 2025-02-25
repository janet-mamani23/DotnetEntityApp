namespace web_api.dto.comment;
using entities_library.login;
using entities_library.movie;

public class CommentResponseDTO
{
    public long Id { get; set; }
    public string ?AvatarUser {get; set;}   
    public string ?UserName { get; set; }   
    public required string Text { get; set; }
    public DateTime CreatedAt { get; set; }
       
   
}
