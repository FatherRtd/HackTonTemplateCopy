using FluentNHibernate.Mapping;
using HackTonTemplate.Models;

namespace HackTonTemplate.Maps;

public class UserMap : ClassMap<User>
{
    public UserMap()
    {
        Id(x => x.Id);

        Map(x => x.Name);
        Map(x => x.Login);
        Map(x => x.Password);
        Map(x => x.UserKey);
        Map(x => x.Role);
        Map(x => x.Mail);
        Map(x => x.Age);
        Map(x => x.Sex);

        References(x => x.Category, "Category_Id");

        HasMany<Skill>(x => x.Skills).Table("Skill").KeyColumn("UserId");
        HasMany(x => x.Achivments).Table("Achivment").KeyColumn("UserId");
    }
}