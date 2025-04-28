using api.Dtos.Account.Propriedade;
using api.Dtos.Avaliacao;
using api.Dtos.Cliente;
using api.Extensions;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{

    [Route("api/avaliacaoGratuita")]
    [ApiController]
    public class AvaliacaoGratuitaController : Controller
    {
        private readonly IAvaliacaoGratuitaRepository _avaiacaoGratuitaRepository;
        private readonly UserManager<AppUser> _userManager;

        public AvaliacaoGratuitaController(UserManager<AppUser> userManager, IAvaliacaoGratuitaRepository avaliacaoGratuitaRepository)
        {
            _avaiacaoGratuitaRepository = avaliacaoGratuitaRepository;
            _userManager = userManager;

        }


        [HttpPost]
            public async Task<IActionResult> Create([FromBody] AvaliacaoGratuita avaliacaoGratuita)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);           
            await _avaiacaoGratuitaRepository.CreateAsync(avaliacaoGratuita);
            return Ok("Cadastro Realizado com Sucesso");

        }

        private object GetById()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);       
            var avaliacao = await _avaiacaoGratuitaRepository.GetAllAsync(query);           

            return Ok(avaliacao);
        }

        [HttpGet]
        [Route("api/avaliacaoGratuita/GetStatus")]
        public async Task<IActionResult> GetStatus([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var avaliacao = await _avaiacaoGratuitaRepository.GetStatus(query);

            return Ok(avaliacao);
        }

        [HttpGet]
        [Route("api/avaliacaoGratuita/GetConsolidado")]
        public async Task<IActionResult> GetConsolidado([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var avaliacao = await _avaiacaoGratuitaRepository.GetConsolidado(query);

            return Ok(avaliacao);
        }

        [HttpPut]
        [Authorize]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] AvaliacaoDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var avaliacao = await _avaiacaoGratuitaRepository.UpdateAsync(id, updateDto);

            if (avaliacao == null)
            {
                return NotFound("Não encontrado");
            }

            return Ok(avaliacao);
        }
    }
}
