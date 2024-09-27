using dao_library.Interfaces;
using dao_library.Interfaces.login;
using dao_library.entity_framework.login;
using dao_library.Interfaces.comment;
using dao_library.entity_framework.comment;
using dao_library.Interfaces.movie;
using dao_library.entity_framework.movie;
using dao_library.Interfaces.qualify;
using dao_library.entity_framework.qualify;
using dao_library.Interfaces.file_system;
using dao_library.entity_framework.file_system;

namespace dao_library.entity_framework;

public class DAOEFFactory : IDAOFactory
{
    private readonly ApplicationDbContext context;

    public DAOEFFactory(ApplicationDbContext context)
    {
        this.context = context;
    }

    public IDAOPerson CreateDAOPerson()
    {
        return new DAOEFPerson(context);
    }

    public IDAOUser CreateDAOUser()
    {
        return new DAOEFUser(context);
    }

    public IDAOUserBan CreateDAOUserBan()
    {
        return new DAOEFUserBan(context);
    }

    public IDAOComment CreateDAOComment()
    {
        return new DAOEFComment(context);
    }

    public IDAOMovie CreateDAOMovie()
    {
        return new DAOEFMovie(context);
    }

    public IDAOGenre CreateDAOGenre()
    {
        return new DAOEFGenre(context);
    }

    public IDAOQualify CreateDAOQualify()
    {
        return new DAOEFQualify(context);
    }

    public IDAOFileEntity CreateDAOFileEntity()
    {
        return new DAOEFFile(context);
    }

    public IDAOFileType CreateDAOFileType()
    {
        return new DAOEFFileType(context);
    }
}
