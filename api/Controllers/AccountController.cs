using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using api.Dtos.Account;
using api.Dtos.Cliente;
using api.Extensions;
using api.Interfaces;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly IClienteRepository _clienteRepo;

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, 
            SignInManager<AppUser> signInManager, IClienteRepository clienteRepo)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signinManager = signInManager;
            _clienteRepo = clienteRepo;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            if (user == null) return Unauthorized("Usuario invalido!");

            var result = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Usuario não encontrado ou senha incorreta");

            return Ok(
                new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                }
            );
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var appUser = new AppUser
                {
                   
                    UserName = registerDto.Username,
                    Email = registerDto.Email                   
                              
                };

             

                    //  var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == registerDto.Username.ToLower());

                    var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);



                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "Admin");
                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto
                            {
                                UserName = appUser.UserName,
                                Email = appUser.Email,
                                Token = _tokenService.CreateToken(appUser)
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("registerCliente")]
        public async Task<IActionResult> RegisterCliente([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var appUser = new AppUser
                {

                    UserName = registerDto.Username,
                    Email = registerDto.Email

                };



                //  var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == registerDto.Username.ToLower());

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);



                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");

                    if (roleResult.Succeeded)
                    {
                        if (registerDto.fullName != null)
                        {
                          
                            var usernameCliente = User.GetUsername();
                            var appUserCliente = await _userManager.FindByNameAsync(usernameCliente);
                            var AppUserId = appUserCliente.Id;

                            var clientedto = new Cliente();
                            clientedto.email = registerDto.Email;
                            clientedto.Nome = registerDto.fullName;
                            clientedto.AppUserId = AppUserId;
                            clientedto.Cpf = "nao_tem";
                            clientedto.Endereco = "nao_tem";
                            await _clienteRepo.CreateAsync(clientedto);

                        }
                        return Ok(
                            new NewUserDto
                            {
                                UserName = appUser.UserName,
                                Email = appUser.Email,
                                Token = _tokenService.CreateToken(appUser)
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}