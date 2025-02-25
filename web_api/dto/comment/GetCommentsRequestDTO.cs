namespace web_api.Controllers.comment;
public class GetCommentsRequestDTO
{
    public required int movieId {get; set;} 
    public int page {get; set;} = 1;
    public int pageSize {get; set;} = 10;

}