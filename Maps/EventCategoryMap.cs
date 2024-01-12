using FluentNHibernate.Mapping;
using HackTonTemplate.Models;

namespace HackTonTemplate.Maps
{
   public class EventCategoryMap : ClassMap<EventCategory>
   {
       public EventCategoryMap()
       {
           Id(x => x.Id);

           Map(x => x.Name);
       }
   }
}
