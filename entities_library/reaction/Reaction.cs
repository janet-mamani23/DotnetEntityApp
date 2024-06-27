using entities_library.login;

namespace entities_library.reaction;

public class Reaction{
    public long Id {get; set;}

    public required User User {get; set;}

    public ReactionType ReactionType {get; set;}
}