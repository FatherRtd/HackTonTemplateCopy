using HackTonTemplate.Maps;

namespace HackTonTemplate.Models
{
    public class User
    {
        public virtual long Id { get; set; }
        public virtual string? Name { get; set; }
        public virtual string? Login { get; set; }
        public virtual string? Password { get; set; }
        public virtual string? UserKey { get; set; }
        public virtual string? Mail { get; set; }
        public virtual int? Age { get; set; }
        public virtual SexType? Sex { get; set; }
        public virtual UserRole Role { get; set; }
        public virtual EventCategory Category { get; set; }
        public virtual IEnumerable<Skill> Skills { get; set; }
        public virtual IEnumerable<Achivment> Achivments { get; set; }
    }
}
