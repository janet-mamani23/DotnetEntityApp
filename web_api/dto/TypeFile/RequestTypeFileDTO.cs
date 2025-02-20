using web_api.dto.common;

namespace web_api.dto.TypeFile;

    public class RequestTypeFileDTO : RequestDTO
    {  
            public long Id { get; set; }
            public required string Name { get; set; }
}