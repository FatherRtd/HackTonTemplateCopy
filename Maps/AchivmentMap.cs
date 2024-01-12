using FluentNHibernate.Mapping;
using HackTonTemplate.Models;

namespace HackTonTemplate.Maps
{
   public class AchivmentMap : ClassMap<Achivment>
   {
       public AchivmentMap()
       {
           Id(x => x.Id).GeneratedBy.Identity(); ;

           Map(x => x.Name);
           Map(x => x.UserId);
           Map(x => x.Year);

           References(x => x.EventType, "EventTypeId");
       }
   }
}
