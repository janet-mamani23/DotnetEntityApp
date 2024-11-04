namespace entities_library.file_system;

public class FileEntity
{
    public long Id {get; set;}

    public required string Path {get; set;}

    public virtual FileType? FileType{get; set;}

    public virtual string GetUrl() 
    {
        if(String.IsNullOrEmpty(this.Path)) 
        {
            return "htttps://urlpaginacualquiera/userdefault.jpg";
        }
        return this.Path;
    }

}