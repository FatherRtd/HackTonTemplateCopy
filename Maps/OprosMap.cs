using FluentNHibernate.Mapping;
using HackTonTemplate.Models;

namespace HackTonTemplate.Maps
{
   public class OprosMap : ClassMap<Opros>
   {
       public OprosMap()
       {
           Id(x => x.Id);

           Map(x => x.Date);
           Map(x => x.HaveCommercialProject);

           References(x => x.User, "User_Id");
       }
   }
}
