using FluentNHibernate.Mapping;
using HackTonTemplate.Models;

namespace HackTonTemplate.Maps
{
    public class UserEventMap : ClassMap<UserEvent>
    {
        public UserEventMap()
        {
            Id(x => x.Id);

            Map(x => x.EventId, "Event_Id");
            Map(x => x.UserId, "User_Id");
        }
    }
}
