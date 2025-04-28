using api.Dtos.Account.Propriedade;
using api.Dtos.Cliente;
using api.Extensions;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/propriedade")]
    [ApiController]
    public class PropriedadeController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IPropriedadeRepository _propriedadeRepo;



        public PropriedadeController(UserManager<AppUser> userManager, IPropriedadeRepository propriedadeRepo)
        {
            _propriedadeRepo = propriedadeRepo;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] PropriedadeDto propriedadeDto)
        {

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var propriedademodel = propriedadeDto.ToPropriedadeFromCreate();
            propriedademodel.AppUserId = appUser.Id;
            await _propriedadeRepo.CreateAsync(propriedademodel);
            return CreatedAtAction(nameof(GetById), new { id = propriedademodel.Id }, propriedademodel.ToPropiedadeDto());



        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var propriedade = await _propriedadeRepo.GetByIdAsync(id);

            if (propriedade == null)
            {
                return Ok(propriedade.ToPropiedadeDto());
            }

            return Ok(propriedade.ToPropiedadeDto());
        }

        [HttpGet]
        [Authorize]
     
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var idCliente = query.idCliente;
            var username = User.GetUsername();        
            var appUser = await _userManager.FindByNameAsync(username);
            var AppUserId = appUser.Id;
        var buscaPropriedade = await _propriedadeRepo.GetAllAsync(AppUserId);
           // var propriedadedto = buscaPropriedade.Select(s => s.ToPropiedadeDto()).ToList();


            return Ok(buscaPropriedade);
        }

        [HttpGet]
        [Route("api/propriedade/BuscaPropriedades")]
        public async Task<IActionResult> BuscaPropriedades()       
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var buscaPropriedade = await _propriedadeRepo.GetAllAsync();

            return Ok(buscaPropriedade);
        }


        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel = await _propriedadeRepo.DeleteAsync(id);

            if (stockModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }


        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PropriedadeDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var propriedade = await _propriedadeRepo.UpdateAsync(id, updateDto);

            if (propriedade == null)
            {
                return NotFound("Não encontrado");
            }

            return Ok(propriedade);
        }


    }
}