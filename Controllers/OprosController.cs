using HackTonTemplate.Models;
using Microsoft.AspNetCore.Mvc;
using HackTonTemplate.Services;

namespace HackTonTemplate.Controllers
{
    public class OprosController : Controller
    {
        private readonly IOprosService _oprosService;

        public OprosController(IOprosService oprosService)
        {
            _oprosService = oprosService;
        }

        [Route("api/skill-type/get")]
        [HttpGet]
        public async Task<IEnumerable<SkillType>> GetSkillTypes()
        {
            return await _oprosService.GetSkillTypes();
        }

        [Route("api/opros/save")]
        [HttpPost]
        public async Task SaveOpros(Opros opros)
        {
            await _oprosService.SaveUserOpros(opros);
        }
    }
}
