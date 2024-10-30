namespace web_api.Controllers.comment;

public class GetCommentsResponseDTO
{
    long id {get; set;}

    public string avatarUser {get; set;} = "";

    public string? nameUser {get; set;}

    public string? textComment {get; set;}
}