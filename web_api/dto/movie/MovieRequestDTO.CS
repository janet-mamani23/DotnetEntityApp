using entities_library.file_system;
using web_api.dto.common;

namespace web_api.dto.movie;

    public class MovieRequestDTO : RequestDTO
    {
        public string? TitleMovie { get; set; }
        public string? DescriptionMovie{ get; set; }
        public long? GenreId {get; set;} = 0;
        public string? ImageUrl { get; set; }//url de la imagen
        public string? VideoUrl { get; set; }
        public List<long> CommentIds { get; set; } = new List<long>();
        public List<long> QualifyIds { get; set; } = new List<long>();
        public bool HasOscar { get; set; }
}