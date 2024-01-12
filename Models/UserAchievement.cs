using HackTonTemplate.Maps;

namespace HackTonTemplate.Models
{
    public class UserAchievement
    {
        public virtual long Id { get; set; }
        public virtual string Title { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string Description { get; set; }
        public virtual CompetitivePlace Place { get; set; }
        public virtual User User { get; set; }
    }
}
