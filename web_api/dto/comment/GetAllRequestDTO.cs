using web_api.dto.common;

namespace web_api.dto.movie
{
    public class GetAllRequestDTO
    {
        public required string Query {get; set;}   //query sirve para filtrar
        public int Page {get; set;} = 1;
        public int PageSize {get; set;} = 10;

    }
}