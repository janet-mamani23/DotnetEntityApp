using entities_library.file_system;
using web_api.dto.common;

namespace web_api.dto.movie;

    public class MovieRequestDTO : RequestDTO
    {
        public string TitleMovie { get; set; } = "";//titulo del post
        public string DescriptionMovie{ get; set; } = "";
        public long GenreId {get; set;} = 0;
        public string? ImageUrl { get; set; }//url de la imagen
        public string? VideoUrl { get; set; }
    
}