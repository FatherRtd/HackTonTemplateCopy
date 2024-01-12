using HackTonTemplate.Maps;

namespace HackTonTemplate.Models
{
    public class Opros
    {
        public virtual long Id { get; set; }
        public virtual User User { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual bool HaveCommercialProject { get; set; }
    }
}
