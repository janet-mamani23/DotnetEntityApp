using web_api.dto.common;

namespace web_api.dto.movie
{
    public class MovieCommentsRequestDTO
    {
        public required string Title {get; set;}   
        public int Page {get; set;} = 1;
        public int PageSize {get; set;} = 10;
    }
}