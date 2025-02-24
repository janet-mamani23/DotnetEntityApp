using entities_library.login;

namespace dao_library.Interfaces.login;

public interface IDAOUserBan
{
    Task<(IEnumerable<UserBan> usersBan, int totalBan)> GetAll();
    Task<UserBan?> GetByName(string name, string lastName);
    Task Save(UserBan userBan);
    
    //NO PROGRAMAR
    Task Delete(long id);
}