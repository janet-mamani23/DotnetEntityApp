using entities_library.login;
using entities_library.movie;

namespace entities_library.Qualify;
public class Qualify
{
    public long Id {get; set;}
    
    public long UserId { get; set; }
    public virtual required User User {get; set;} 

    public required int Stars {get; set;}

    public long MovieId { get; set; }

    public virtual required Movie Movie {get; set;}
}