using dao_library.Interfaces.file_system;
using entities_library.file_system;
using Microsoft.EntityFrameworkCore;

namespace dao_library.entity_framework.ef_file_system;

public class DAOEFFileType: IDAOFileType
{
    //TODO COMPLETAR METODOS
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

    public async Task<FileType?> GetById(long? id)
    {
        if (id == null) return null;
        if (context.FileTypes == null) return null;
        FileType? fileTypeId = await context.FileTypes
            .Where(fileTypeId => fileTypeId.Id == id)
            .FirstOrDefaultAsync();

        return fileTypeId;
    }

    public async Task<bool> Save(FileType fileType)
    {
        var succes = false;
        try{
            context.FileTypes?.Add(fileType);
            await context.SaveChangesAsync(); 
            succes = true;
            return succes; 
            }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving FileType: {ex.Message}");
            return succes;
        }
    }
}