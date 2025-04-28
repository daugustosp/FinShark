using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Cliente;
using api.Dtos.Comment;
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
    [Route("api/cliente")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IClienteRepository _clienteRepo;
       
     
        public ClienteController(UserManager<AppUser> userManager, IClienteRepository clienteRepo)
        {
            _clienteRepo = clienteRepo;
            _userManager = userManager;
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cliente = await _clienteRepo.GetByIdAsync(id);

            if (cliente == null)
            {
                return Ok(cliente.ToClienteDto());
            }

            return Ok(cliente.ToClienteDto());
        }


      [HttpPost]
      [Authorize]   
      public async Task<IActionResult> Create([FromBody] CreateClienteDto clienteDto)
        {

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var clienteModel = clienteDto.ToCLienteFromCreate();
            clienteModel.AppUserId = appUser.Id;

             await _clienteRepo.CreateAsync(clienteModel);

            return CreatedAtAction(nameof(GetById), new { id = clienteModel.id }, clienteModel.ToClienteDto());

            
               
         }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var AppUserId = appUser.Id;
            var clientes = await _clienteRepo.GetAllAsync(AppUserId);

            var clienteDto = clientes.Select(s => s.ToClienteDto()).ToList();

            return Ok(clienteDto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel = await _clienteRepo.DeleteAsync(id);

            if (stockModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateClienteDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cliente = await _clienteRepo.UpdateAsync(id, updateDto);

            if (cliente == null)
            {
                return NotFound("Não encontrado");
            }

            return Ok(cliente.ToClienteDto());
        }

    }
}