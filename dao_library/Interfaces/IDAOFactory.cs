using dao_library.Interfaces.login;
using dao_library.Interfaces.comment;
using dao_library.Interfaces.movie;
using dao_library.Interfaces.qualify;
using dao_library.Interfaces.file_system;

namespace dao_library.Interfaces;

public interface IDAOFactory
{
    IDAOUser CreateDAOUser();

    IDAOPerson CreateDAOPerson();

    IDAOUserBan CreateDAOUserBan();

    IDAOComment CreateDAOComment();

    IDAOMovie CreateDAOMovie();

    IDAOGenre CreateDAOGenre();

    IDAOQualify CreateDAOQualify();

    IDAOFileEntity CreateDAOFileEntity();

    IDAOFileType CreateDAOFileType();

}