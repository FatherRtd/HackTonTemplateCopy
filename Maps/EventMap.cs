using FluentNHibernate.Mapping;
using HackTonTemplate.Models;

namespace HackTonTemplate.Maps
{
   public class EventMap : ClassMap<Event>
   {
       public EventMap()
       {
           Id(x => x.Id);

           Map(x => x.Name);
           Map(x => x.Address);
           Map(x => x.Desctiption);
           Map(x => x.StartDate);
           Map(x => x.EndDate);
           Map(x => x.ImageUrl);
           Map(x => x.SiteUrl);
           Map(x => x.IsOnline);

           References(x => x.Category, "Category_Id");
           References(x => x.Type, "Type_Id");
           References(x => x.CreateUser, "User_Id");
       }
   }
}
