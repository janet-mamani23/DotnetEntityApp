namespace entities_library.login;

public class UserBan{

    public long Id {get; set;}

    public DateTime? StartDateTime {get; set;}

    public DateTime? EndtDateTime {get; set;}

    public virtual required User User {get; set;}

    public string? Reason {get; set;} //Razon por la cual se banea.


public void SetBanDuration()
    {
        if (!StartDateTime.HasValue)
        {
            StartDateTime = DateTime.Now;
        }

        switch (Reason)
        {
            case "Abusivo, racista":
                EndtDateTime = StartDateTime.Value.AddDays(7);
                break;
            case "Publicidad no autorizada":
                EndtDateTime = StartDateTime.Value.AddDays(3);
                break;
            default:
                EndtDateTime = StartDateTime.Value.AddDays(1); 
                break;
        }
    }
public bool CheckBanStatus()
    {
        if (EndtDateTime.HasValue && EndtDateTime.Value <= DateTime.Now)
        {
            User.UserStatus = UserStatus.Active;
            return true;
        }
        return false;
    }
public void DissBanned()
{
    User.UserStatus = UserStatus.Active;
}
}