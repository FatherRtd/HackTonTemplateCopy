using FluentNHibernate.Mapping;
using HackTonTemplate.Models;

namespace HackTonTemplate.Maps
{
   public class SkillTypeMap : ClassMap<SkillType>
   {
       public SkillTypeMap()
       {
           Id(x => x.Id);

           Map(x => x.Name);
       }
   }
}
