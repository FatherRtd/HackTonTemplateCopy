using FluentNHibernate.Mapping;
using HackTonTemplate.Models;

namespace HackTonTemplate.Maps
{
   public class SkillMap : ClassMap<Skill>
   {
       public SkillMap()
       {
           Id(x => x.Id);

           Map(x => x.AgeExperience);
           Map(x => x.UserId, "User_Id");

           References(x => x.Type, "Type_Id");
       }
   }
}
