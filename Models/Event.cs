namespace HackTonTemplate.Models
{
    public class Event
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string? Desctiption { get; set; }
        public virtual string? Address { get; set; }
        public virtual string? ImageUrl { get; set; }
        public virtual string? SiteUrl { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual bool IsOnline { get; set; }
        public virtual EventType Type { get; set; }
        public virtual EventCategory Category { get; set; }
        public virtual User CreateUser { get; set; }

        public virtual bool UserIsRegister { get; set; }
    }
}
