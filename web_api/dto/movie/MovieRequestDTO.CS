using web_api.dto.common;

namespace web_api.dto.movie;

    public class MovieRequestDTO : RequestDTO
    {
        public string? TitleMovie { get; set; }
        public string? DescriptionMovie{ get; set; }
        public long? GenreId {get; set;}
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public bool HasOscar { get; set; } = false;
}