using dao_library.Interfaces.file_system;
using entities_library.file_system;

namespace dao_library.entity_framework.file_system;

public class DAOEFFile: IDAOFileEntity
{
    private readonly ApplicationDbContext context;

    public DAOEFFile(ApplicationDbContext context)
    {
        this.context = context;
    }

    public Task Delete(FileEntity fileEntity)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FileEntity>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<FileEntity> GetById(long id)
    {
        throw new NotImplementedException();
    }

    public Task Save(FileEntity fileEntity)
    {
        throw new NotImplementedException();
    }
}