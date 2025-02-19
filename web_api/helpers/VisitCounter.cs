namespace web_api.helpers;

public class VisitCounter
{
    private static VisitCounter? instante = null;

    private VisitCounter()
    {
        this.Number = 0;
    }

    //Método de clase.
    public static VisitCounter GetInstance()
    {
        instante ??= new VisitCounter();
        return instante;
    }

    private long Number { get; set; }

    public long GetNextNumber()
    {
        this.Number++;
        return this.Number;
    }

    public long GetNumber()
    {
        return this.Number;
    }
    public long GetRestarNumber()
    {
        return --this.Number;
    }
}