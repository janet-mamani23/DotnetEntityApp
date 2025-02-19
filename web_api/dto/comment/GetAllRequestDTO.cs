using web_api.dto.common;

namespace web_api.dto.movie
{
    public class GetAllRequestDTO
    {
        public required string query {get; set;}   //query sirve para filtrar
        public int page {get; set;} = 1;
        public int pageSize {get; set;} = 10;

    }
}