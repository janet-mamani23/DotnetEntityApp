using web_api.dto.common;
using web_api.dto.movie;
namespace web_api.dto.login;

public class ResponsePostGenreDTO : ResponseDTO
{
    public string genreName {get; set;} = "";
}