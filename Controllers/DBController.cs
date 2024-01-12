using Microsoft.AspNetCore.Mvc;
using HackTonTemplate.Models;
using HackTonTemplate.Services;
using NHibernate.Linq;
using ISession = NHibernate.ISession;

namespace HackTonTemplate.Controllers
{
    public class DBController : Controller
    {
        private readonly ILogger<DBController> _logger;
        private readonly IUserService _userService;
        private readonly ISession _session;

        public DBController(
            ILogger<DBController> logger, 
            IUserService userService, 
            ISession session
            )
        {
            _logger = logger;
            _userService = userService;
            _session = session;
        }

        [HttpGet("DBController/AddUserTestDB")]
        public async Task<IActionResult> AddUserTestDB()
        {
            var user = new User
            {
                Login = "admin1",
                Password = "admin1"
            };  

            user = await _userService.AddUser(user, UserRole.Administrator);

            using var db = _session.BeginTransaction();

            var eventTypes = new List<EventType>()
            {
                new()
                {
                    Id = 1,
                    Name = "Хакатон"
                },
                new()
                {
                    Id = 2,
                    Name = "Форум"
                },
                new()
                {
                    Id = 3,
                    Name = "Грант"
                },
                new()
                {
                    Id = 4,
                    Name = "Конкурс"
                },
            };

            var eventCategory = new List<EventCategory>()
            {
                new()
                {
                    Id = 1,
                    Name = "Начинающий"
                },
                new()
                {
                    Id = 2,
                    Name = "Средний"
                },
                new()
                {
                    Id = 3,
                    Name = "Профи"
                },
                new()
                {
                    Id = 4,
                    Name = "Для всех"
                }
            };

            var skillTypes = new List<SkillType>()
            {
                new()
                {
                    Id = 264,
                    Name = "JavaScript"
                },
                new()
                {
                    Id = 322,
                    Name = "SQL"
                },
                new()
                {
                    Id = 1017,
                    Name = "HTML"
                },
                new()
                {
                    Id = 446,
                    Name = "Python"
                },
                new()
                {
                    Id = 32,
                    Name = "CSS"
                },
                new()
                {
                    Id = 31,
                    Name = "ООП"
                },
                new()
                {
                    Id = 1070,
                    Name = "React"
                },
                new()
                {
                    Id = 1067,
                    Name = "Docker"
                },
                new()
                {
                    Id = 1012,
                    Name = "Java"
                },
                new()
                {
                    Id = 1005,
                    Name = "PHP"
                },
                new()
                {
                    Id = 245,
                    Name = "TypeScript"
                },
                new()
                {
                    Id = 706,
                    Name = "C#"
                },
                new()
                {
                    Id = 318,
                    Name = "Веб-разработка"
                }
            };

            var events = new List<Event>()
            {
                new()
                {
                    Id = 1,
                    Name = "Хантатон",
                    Desctiption =
                        "Восьмой окружной конкурс разработчиков мобильных приложений и веб-сервисов #Хантатон 2023\r\nВ этом году «марафон программистов» будет проходить в двух форматах онлайн для ЮНИОРОВ и оффлайн для ПРОФИ на базе уникального образовательного проекта Сбера «Школы 21» в г.Сургут",
                    Address = "г. Сургут, ​Улица Иосифа Каролинского, 14/1",
                    ImageUrl =
                        "https://admmegion.ru/upload/iblock/f75/jzqlcyptyxffh63rkvvvla592okybws3/brqgs51s1zo38jh4lc7rbc80we6gmcxd.jpg",
                    SiteUrl = "https://hackathon.uriit.ru/2023/",
                    CreateUser = user,
                    StartDate = new DateTime(2023, 11, 21),
                    EndDate = new DateTime(2023, 11, 26),
                    IsOnline = false,
                    Type = eventTypes.First(),
                    Category = eventCategory.Last()
                },
                new()
                {
                    Id = 2,
                    Name = "Международный хакатон",
                    Desctiption = "Международный хакатон – это самое масштабное и первое соревнование такого уровня в истории проекта. Участники со всего мира в командах от трех до пяти человек решат 10 кейсов от бизнеса и государства. Мероприятие пройдет в гибридном формате: очно в Мастерской управления Сенеж, ИТ-хабах по всей стране или онлайн.",
                    Address = "",
                    ImageUrl = "https://static.tildacdn.com/tild6366-3636-4632-a131-653935613565/_.jpg",
                    SiteUrl = "https://hacks-ai.ru/hackathons.html?eventId=969092",
                    StartDate = new DateTime(2023, 11, 30),
                    EndDate = new DateTime(2023, 12, 1),
                    IsOnline = true,
                    CreateUser = user,
                    Type = eventTypes.First(),
                    Category = eventCategory.Last()
                }
            };

            var achiv = new Achivment
            {
                Id = 1,
                UserId = user.Id,
                Name = "Хантатон",
                Year = 2023,
                EventType = eventTypes.First()
            };

            eventTypes.ForEach(x => _session.Save(x));
            eventCategory.ForEach(x => _session.Save(x));
            skillTypes.ForEach(x => _session.Save(x));
            events.ForEach(x => _session.Save(x));

            await _session.SaveAsync(achiv);

            await db.CommitAsync();

            var massege = new ContentResult
            {
                Content = "Тестовые данные добавленны User",
            };
            return massege;
        }
    }
}