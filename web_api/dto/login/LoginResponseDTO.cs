using web_api.dto.common;

namespace web_api.dto.login;


public class LoginResponseDTO : ResponseDTO
{
    public long Id {get; set;}
    public string NameUser {get; set;} = "";

    public string LastnameUser {get; set;} = "";

    public string DescriptionUser {get; set;} = "";

    public string UrlAvatar{get; set;} = "";

    public string EmailUser {get; set;} = "";

}