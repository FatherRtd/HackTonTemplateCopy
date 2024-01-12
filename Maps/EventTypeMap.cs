using FluentNHibernate.Mapping;
using HackTonTemplate.Models;

namespace HackTonTemplate.Maps
{
   public class EventTypeMap : ClassMap<EventType>
   {
       public EventTypeMap()
       {
           Id(x => x.Id);

           Map(x => x.Name);
       }
   }
}
