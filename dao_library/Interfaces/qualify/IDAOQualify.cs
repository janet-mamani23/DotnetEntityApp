using entities_library.Qualify;

namespace dao_library.Interfaces.qualify;

public interface IDAOQualify
{
    Task<Qualify> GetById(long id);
    Task Save(Qualify qualify);
    
    //NO PROGRAMAR
    Task Delete(Qualify qualify);
}