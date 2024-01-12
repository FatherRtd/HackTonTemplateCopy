namespace HackTonTemplate.Models;

public class Achivment
{
    public virtual long Id { get; set; }
    public virtual long UserId { get; set; }
    public virtual long Year { get; set; }
    public virtual string Name { get; set; }
    public virtual EventType EventType { get; set; }
}