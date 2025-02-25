using web_api.dto.common;

namespace web_api.dto.movie;

    public class MovieRequestUpdateDTO : RequestDTO
    {
        public long MovieId {get;set;}
        public string? TitleMovie { get; set; } = null;
        public string? DescriptionMovie{ get; set; } = null;
        public long? GenreId {get; set;} = null;
        public string? ImageUrl { get; set; } = null;
        public string? VideoUrl { get; set; } = null;
        public bool HasOscar { get; set; } 
}