namespace web_api.dto.comment;
public class RequestPostCommentDTO
{
    public required long IdUser { get; set; } 
    public long IdMovie { get; set; } 
    public required string Text { get; set; }
    public DateTime CreatedAt { get; set; }
}
