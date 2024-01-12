using HackTonTemplate.Extensions;
using HackTonTemplate.Models;
using ISession = NHibernate.ISession;
using NHibernate.Linq;

namespace HackTonTemplate.Services
{
    public class UserService : IUserService
    {
        private readonly ISession _session;
        public UserService(ISession session)
        {
            _session = session;
        }

        public async Task<User?> GetUserByLoginData(LoginOrRegistrationData loginOrRegistrationData)
        {
            using (var db = _session.BeginTransaction())
            {
                return await _session.Query<User>().FirstOrDefaultAsync(x =>
                    x.Login == loginOrRegistrationData.Login && x.Password == loginOrRegistrationData.Password);
            }
        }

        public async Task<User?> GetUserByKey(string userKey)
        {
            using (var db = _session.BeginTransaction())
            {
                return await _session.Query<User>().FirstOrDefaultAsync(x => x.UserKey == userKey);
            }
        }

        public async Task<bool> IsLoginBusy(string login)
        {
            using (var db = _session.BeginTransaction())
            {
                return await _session.Query<User>().AnyAsync(x => x.Login == login);
            }
        }

        public async Task<User> AddUser(User user, UserRole role)
        {
            using (var db = _session.BeginTransaction())
            {
                var guid = Guid.NewGuid();
                var userKey = user.Login.ToSha256() + guid;

                var newUser = new User
                {
                    Login = user.Login,
                    Password = user.Password,
                    Mail = user.Mail,
                    Age = user.Age,
                    Sex = user.Sex,
                    Name = user.Name,
                    UserKey = userKey,
                    Role = role
                };

                await _session.SaveAsync(newUser);
                await db.CommitAsync();

                return await _session.Query<User>().SingleAsync(x => x.UserKey == userKey);
            }
        }

        public async Task<User?> GetUser(long userId)
        {
            using (var db = _session.BeginTransaction())
            {
                var user = await _session.Query<User>().FirstOrDefaultAsync(x => x.Id == userId);
                return user;
            }
        }

        public async Task EventRegistration(long eventId, long userId)
        {
            using var db = _session.BeginTransaction();

            await _session.SaveAsync(new UserEvent
            {
                UserId = userId,
                EventId = eventId
            });

            await db.CommitAsync();
        }

        public async Task EventUnRegistration(long eventId, long userId)
        {
            using var db = _session.BeginTransaction();

            await _session.Query<UserEvent>()
                          .Where(x => x.EventId == eventId && x.UserId == userId)
                          .DeleteAsync();

            await db.CommitAsync();
        }
    }

    public interface IUserService
    {
        Task EventUnRegistration(long eventId, long userId);
        Task EventRegistration(long eventId, long userId);
        Task<User?> GetUserByLoginData(LoginOrRegistrationData loginOrRegistrationData);
        Task<User?> GetUserByKey(string userKey);
        Task<User?> GetUser(long userId);
        Task<bool> IsLoginBusy(string login);
        Task<User> AddUser(User user, UserRole role);
    }
}
