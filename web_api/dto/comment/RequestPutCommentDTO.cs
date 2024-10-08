namespace web_api.dto.comment;
public class RequestPutCommentDTO
{
    public long idComment { get; set; } 
    public required string text { get; set; }
}