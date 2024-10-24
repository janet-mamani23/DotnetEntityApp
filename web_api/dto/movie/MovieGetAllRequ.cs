using web_api.dto.common;

namespace web_api.dto.movie;
{
    public class MovieGetAllRequestDTO: ResponsetDTO
    {
        public string? query {get; set;} = null;   //query sirve para filtrar
        public int page {get; set;} = 1;
        public int pageSize {get; set;} = 10;

    }
}