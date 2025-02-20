using dao_library.Interfaces.file_system;
using entities_library.file_system;

namespace dao_library.entity_framework.ef_file_system;

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

    public async Task<FileEntity> Save(FileEntity fileEntity)
    {
        if (context.Files == null){
            throw new Exception ("error de acceso a la base de datos");
        }
        context.Files.Add(fileEntity);
        await context.SaveChangesAsync();
        return fileEntity;
    }

}