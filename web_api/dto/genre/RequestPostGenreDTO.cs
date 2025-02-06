using web_api.dto.common;
namespace web_api.dto.genre;

    public class RequestPostGenreDTO : RequestDTO
    {  
            public long Id { get; set; }
            public string Name { get; set; }  = "";
}