using entities_library.file_system;

namespace dao_library.Interfaces.file_system;

public interface IDAOFileEntity
{
    Task<IEnumerable<FileEntity>> GetAll();
    Task<FileEntity> GetById(long id);
    Task Save(FileEntity fileEntity);
    
    //NO PROGRAMAR
    Task Delete(FileEntity fileEntity);
}