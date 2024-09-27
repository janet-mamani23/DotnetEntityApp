using dao_library.Interfaces.file_system;
using entities_library.file_system;

namespace dao_library.entity_framework.file_system;

public class DAOEFFileType: IDAOFileType
{
    private readonly ApplicationDbContext context;

    public DAOEFFileType(ApplicationDbContext context)
    {
        this.context = context;
    }

    public Task Delete(FileType fileType)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FileType>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<FileType> GetById(long id)
    {
        throw new NotImplementedException();
    }

    public Task Save(FileType fileType)
    {
        throw new NotImplementedException();
    }
}