namespace web_api.dto.comment;
public class RequestPostCommentDTO
{
    public required long idUser { get; set; } 
    public long idMovie { get; set; } 
    public required string text { get; set; }
    public DateTime createdAt { get; set; }
}
