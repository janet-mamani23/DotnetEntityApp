using web_api.dto.common;

namespace web_api.dto.TypeFile;

    public class ResponseTypeFileDTO : ResponseDTO
    {  
            public long Id { get; set; }
            public required string TypeName { get; set; }
}