namespace web_api.dto.comment;
public class RequestPutCommentDTO
{
    public long IdComment { get; set; } 
    public required string Text { get; set; }
}