namespace HackTonTemplate.Models
{
    public class UserEvent
    {
        public virtual long Id { get; set; }
        public virtual long UserId { get; set; }
        public virtual long EventId { get; set; }
    }
}
