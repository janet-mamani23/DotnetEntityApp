namespace web_api.dto.comment;
using entities_library.login;
using entities_library.movie;

public class ResponseCommentDTO
{
    public long Id { get; set; }
    public string ?AvatarUser {get; set;}   // URL o ruta del avatar del usuario
    public string ?UserName { get; set; }   // Nombre del usuario
    public required string Text { get; set; }
    public DateTime CreatedAt { get; set; }
       
   
}
