namespace HackTonTemplate.Models;

public class Skill
{
    public virtual long Id { get; set; }
    public virtual long UserId { get; set; }
    public virtual int AgeExperience { get; set; }
    public virtual SkillType Type { get; set; }
}