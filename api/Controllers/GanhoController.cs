using api.Dtos.Cliente;
using api.Dtos.Ganhos;
using api.Extensions;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace api.Controllers
{

    [Route("api/ganho")]
    [ApiController]
    public class GanhoController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IGanhosRepository _ganhosRepo;



        public GanhoController(UserManager<AppUser> userManager, IGanhosRepository ganhosRepo)
        {
            _ganhosRepo = ganhosRepo;
            _userManager = userManager;
            

        }


        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ganhos = await _ganhosRepo.GetByIdAsync(id);

            if (ganhos == null)
            {
                return Ok(ganhos.ToGanhosDto());
            }

            return Ok(ganhos.ToGanhosDto());
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateGanhosDto ganhosDto)
        {

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var GanhoModel = ganhosDto.ToCLienteFromCreate();
            GanhoModel.AppUserId = appUser.UserName;

            GanhoModel.TaxaServiço = 16;
             var calculotaxa = ((ganhosDto.GanhoBruto * GanhoModel.TaxaServiço)/100);
            GanhoModel.TotalLiquido = ganhosDto.GanhoBruto - calculotaxa;
            GanhoModel.TaxaManutencao =  ganhosDto.GanhoBruto - GanhoModel.TotalLiquido; 

            await _ganhosRepo.CreateAsync(GanhoModel);

            return CreatedAtAction(nameof(GetById), new { id = GanhoModel.Id }, GanhoModel.ToGanhosDto());
                    }
      




        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ganhos = await _ganhosRepo.GetAllAsync(query);

            var ganhostdo = ganhos.Select(s => s.ToGanhosDto()).ToList();

            return Ok(ganhostdo);
        }

        [HttpDelete]
        [Authorize]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ganhosmodel = await _ganhosRepo.DeleteAsync(id);

            if (ganhosmodel == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut]
        [Authorize]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateGanhosDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ganhos = await _ganhosRepo.UpdateAsync(id, updateDto);

            if (ganhos == null)
            {
                return NotFound("Não encontrado");
            }

            return Ok(ganhos.ToGanhosDto());
        }

    }






}

