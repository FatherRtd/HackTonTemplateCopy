using HackTonTemplate.Models;
using ISession = NHibernate.ISession;
using NHibernate.Linq;

namespace HackTonTemplate.Services
{
    public interface IOprosService
    {
        Task SaveUserOpros(Opros opros);
        Task<IEnumerable<SkillType>> GetSkillTypes();
    }

    public class OprosService : IOprosService
    {
        private readonly ISession _session;
        public OprosService(ISession session)
        {
            _session = session;
        }

        public async Task SaveUserOpros(Opros opros)
        {
            using var db = _session.BeginTransaction();
            await _session.SaveAsync(opros);

            foreach (var userAchivment in opros.User.Achivments)
            {
                await _session.SaveAsync(userAchivment);
            }

            foreach (var userSkill in opros.User.Skills)
            {
                await _session.SaveAsync(userSkill);
            }
        }

        public async Task<IEnumerable<SkillType>> GetSkillTypes()
        {
            using var db = _session.BeginTransaction();

            return await _session.Query<SkillType>().ToListAsync();
        }
    }
}
