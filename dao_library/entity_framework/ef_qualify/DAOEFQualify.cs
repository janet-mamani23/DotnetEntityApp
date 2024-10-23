using dao_library.Interfaces.qualify;
using entities_library.Qualify;

namespace dao_library.entity_framework.ef_qualify;

public class DAOEFQualify: IDAOQualify
{
    private readonly ApplicationDbContext context;

    public DAOEFQualify(ApplicationDbContext context)
    {
        this.context = context;
    }

    public Task Delete(Qualify qualify)
    {
        throw new NotImplementedException();
    }

    public Task<Qualify> GetById(long id)
    {
        throw new NotImplementedException();
    }

    public Task Save(Qualify qualify)
    {
        throw new NotImplementedException();
    }
    //TODO-LARA: agregar metodo de actualizar calificacion.
}