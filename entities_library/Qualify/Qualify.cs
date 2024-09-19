using entities_library.login;

namespace entities_library.Qualify;
public class Qualify
{
    public long Id {get; set;}
    
    public required User User {get; set;}

    public required int Star {get; set;}
}