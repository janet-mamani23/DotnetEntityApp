using entities_library.file_system;

namespace dao_library.Interfaces.file_system;

public interface IDAOFileType
{
    Task<IEnumerable<FileType>> GetAll();
    Task<FileType?> GetById(long? id);
    Task<bool>Save(FileType fileType);
    
    //NO PROGRAMAR
    Task Delete(FileType fileType);
}